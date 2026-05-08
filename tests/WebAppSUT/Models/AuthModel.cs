using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppSUT.Models
{
    public class AuthModel
    {
        public string? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
