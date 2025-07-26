namespace Furniqo.Auth.API.DTOs
{
    public class OtpLoginRequest
    {
        public string PhoneNumber { get; set; } = null!;
        public string OtpCode { get; set; } = null!;
    }
}
