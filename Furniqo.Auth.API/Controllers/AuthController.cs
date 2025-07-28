using Furniqo.Auth.API.Configurations;
using Furniqo.Auth.API.Models;
using Furniqo.Auth.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Furniqo.Auth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly JwtService _jwt;
        private readonly OtpService _otp;

        public AuthController(AppDbContext db, JwtService jwt, OtpService otp)
        {
            _db = db;
            _jwt = jwt;
            _otp = otp;
        }

        [HttpPost("request-otp")]
        public async Task<IActionResult> RequestOtp([FromBody] string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return BadRequest("Phone number is required.");

            await _otp.CreateOtpSessionAsync(phoneNumber);
            return Ok("OTP sent successfully.");
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] OtpLoginRequest request)
        {
            if (!await _otp.ValidateOtpAsync(request.PhoneNumber, request.OtpCode))
                return Unauthorized("Invalid or expired OTP.");

            var user = await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);
            if (user == null)
            {
                user = new User
                {
                    Id = Guid.NewGuid(),
                    PhoneNumber = request.PhoneNumber,
                    Role = "Customer",
                    IsActive = true
                };
                _db.Users.Add(user);
                await _db.SaveChangesAsync();
            }

            var accessToken = _jwt.GenerateAccessToken(user);
            var refreshToken = _jwt.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);

            if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
                return Unauthorized("Invalid or expired refresh token.");

            var newAccessToken = _jwt.GenerateAccessToken(user);
            var newRefreshToken = _jwt.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }
#if DEBUG
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = await _db.Users.FindAsync(Guid.Parse(userId!));
            if (user == null) return NotFound();

            return Ok(new
            {
                user.Id,
                user.PhoneNumber,
                user.Role,
                user.IsActive
            });
        }
#endif

        [HttpGet("all-users")]
        public IActionResult GetAllUsers()
        {
            var users = _db.Users.ToList();
            return Ok(users);
        }

        [HttpGet("all-otp-sessions")]
        public IActionResult GetAllOtpSessions()
        {
            var sessions = _db.OtpSessions.ToList();
            return Ok(sessions);
        }
    }

    public class OtpLoginRequest
    {
        public string PhoneNumber { get; set; } = null!;
        public string OtpCode { get; set; } = null!;
    }

    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; } = null!;
    }
}
