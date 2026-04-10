namespace School.ViewModels
{
    public class AuthViewModel
    {
        public string? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
