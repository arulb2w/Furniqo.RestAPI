using Furniqo.Auth.API.Models;
using Furniqo.Auth.API.Configurations;

namespace Furniqo.Auth.API.Services
{
    public class OtpService
    {
        private readonly AppDbContext _db;

        public OtpService(AppDbContext db)
        {
            _db = db;
        }

        public string GenerateOtp()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public async Task<OtpSession> CreateOtpSessionAsync(string phoneNumber)
        {
            var otp = GenerateOtp();
            var otpSession = new OtpSession
            {
                Id = Guid.NewGuid(),
                PhoneNumber = phoneNumber,
                OtpCode = otp,
                ExpiryTime = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false
            };

            _db.OtpSessions.Add(otpSession);
            await _db.SaveChangesAsync();

            Console.WriteLine($"🔐 OTP for {phoneNumber}: {otp}");

            return otpSession;
        }

        public async Task<bool> ValidateOtpAsync(string phoneNumber, string otp)
        {
            var session = _db.OtpSessions
                .Where(s => s.PhoneNumber == phoneNumber && s.OtpCode == otp && !s.IsUsed)
                .OrderByDescending(s => s.ExpiryTime)
                .FirstOrDefault();

            if (session == null || session.ExpiryTime < DateTime.UtcNow)
                return false;

            session.IsUsed = true;
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
