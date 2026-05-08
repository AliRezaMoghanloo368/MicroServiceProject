using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppSUT.Helper
{
    public sealed class Urls
    {
        public static string BaseUrl = "https://localhost:7207/";

        public sealed class Identity
        {
            public static string UserNameUrl { get; set; } = "users/name/{userName}";
        }
    }
}
