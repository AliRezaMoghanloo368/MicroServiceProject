namespace Identity.Domain.Application.Services.Authenticate
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        public int ExpiryMinutes { get; set; }
        public string Issuer { get; set; } //کدام سرویس ایجاد کرده
    }
}
