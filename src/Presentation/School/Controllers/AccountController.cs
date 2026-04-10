using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using School.Services;
using School.ViewModels;
using System.Security.Claims;

namespace School.Controllers
{
    public class AccountController : Controller
    {
        private readonly RequestService _service;
        private readonly NavigationManager _nav;
        public AccountController(RequestService service, NavigationManager nav)
        {
            _service = service;
            _nav = nav;
        }

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _service.GetAsync<AuthViewModel>($"users/name/{model.UserName}");

            if (result.Success == true)
            {
                ModelState.AddModelError("PhoneNumber", "شماره تماس وارد شده قبلا ثبت نام کرده است");
                return View(model);
            }
            CreateUserViewModel user = new CreateUserViewModel()
            {
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password,
            };
            await _service.PostAsync<AuthViewModel>($"register", user);
            return View("SuccessRegister", model);
        }
        #endregion

        #region Login
        [HttpGet("Account/Login")]
        public void Login()
        {
            _nav.NavigateTo("/login");
        }

        [HttpPost("Account/Login")]
        public async void Login(AuthViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            var result = await _service.PostAsync<bool>($"login", model);
            if (result == null)
            {
                //ModelState.AddModelError("UserName", "اطلاعات صحیح نیست");
                return;
            }

            var uResult = await _service.GetAsync<AuthViewModel>($"users/name/{model.UserName}");
            var user = uResult.Data;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Sid, user.PhoneNumber),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe
            };
            await HttpContext.SignInAsync(principal, properties);
            //HttpContext.Request.Headers.Authorization = "";
            _nav.NavigateTo("/");
        }
        #endregion

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
        }
    }
}
