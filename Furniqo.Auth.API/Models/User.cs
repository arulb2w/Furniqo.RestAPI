namespace Furniqo.Auth.API.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string Role { get; set; } = "Customer"; // Admin, Customer, etc.
        public bool IsActive { get; set; } = true;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class OtpSession
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string OtpCode { get; set; } = null!;
        public DateTime ExpiryTime { get; set; }
        public bool IsUsed { get; set; } = false;
    }
}
