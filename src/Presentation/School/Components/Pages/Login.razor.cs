using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using School.ViewModels;

namespace School.Components.Pages
{
    public partial class Login
    {
        private byte[]? profileImage;
        private AuthViewModel authViewModel = new();
        //private GetUserDto? user;
        void RegisterPage()
        {
            _navigator.NavigateTo("/register");
        }

        async Task SignIn()
        {
            if (authViewModel.UserName == null || authViewModel.Password == null)
            {
                await _js.InvokeVoidAsync("showSnackbar", "نام کاربری و رمز عبور نامعتبر است. 😔");
                return;
            }

            var result = await _authService.LoginAsync(authViewModel.UserName, authViewModel.Password);
            if (result)
            {
                await _js.InvokeVoidAsync("showSnackbar", "خوش آمدید ❤️🌺");
                Thread.Sleep(1000);
                _navigator.NavigateTo("/desktop");
            }
            else
            {
                await _js.InvokeVoidAsync("showSnackbar", "نام کاربری و رمز عبور نامعتبر است. 😔");
            }
        }

        async Task CloseWindow()
        {
            await _js.InvokeVoidAsync("closeWindow");
        }

        async Task LoadProfileImage(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                if (authViewModel.UserName == null)
                {
                    await _js.InvokeVoidAsync("showSnackbar", "نام کاربری و رمز عبور نامعتبر است. 😔");
                    return;
                }

                await _js.InvokeVoidAsync("passwordFocus");

                authViewModel = await _authService.GetUserAsync(authViewModel.UserName);
                if (authViewModel?.Id?.ToString() != null)
                {
                    FileViewModel fileViewModel = new FileViewModel() { EntityName = "Users", EntityId = authViewModel.Id.ToString() };
                    var files = await _filesService.LoadAsync(fileViewModel);
                    if (files != null && files?.Data.Count > 0)
                    {
                        foreach (var file in files.Data)
                        {
                            profileImage = file.FileContent;
                        }
                    }
                }
                else
                    profileImage = null;
            }
        }

        async Task GotoDesktop(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await SignIn();
            }
        }
    }
}
