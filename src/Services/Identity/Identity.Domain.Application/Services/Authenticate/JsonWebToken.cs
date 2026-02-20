namespace Identity.Domain.Application.Services.Authenticate
{
    public class JsonWebToken
    {
        public string Token { get; set; }
        public long Expires { get; set; }
    }
}
