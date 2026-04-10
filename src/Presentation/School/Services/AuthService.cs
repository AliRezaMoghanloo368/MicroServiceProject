using Blazored.LocalStorage;
using School.ViewModels;
using SharedLibrary.Patterns.ResultPattern;

namespace School.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        public AuthService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorage)
        {
            _httpClient = httpClientFactory.CreateClient("API/Users");
            _localStorage = localStorage;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var request = new AuthViewModel { UserName = username, Password = password };
            var response = await _httpClient.PostAsJsonAsync("login", request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Result<string>>();
                if (!string.IsNullOrEmpty(result.Data))
                {
                    await _localStorage.SetItemAsync("schoolAuthToken", result.Data);
                    return true;
                }
            }
            return false;
        }

        public async Task<Result<CreateUserViewModel>> RegisterAsync(CreateUserViewModel createUser)
        {
            var response = await _httpClient.PostAsJsonAsync("register", createUser);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Result<CreateUserViewModel>>();
                return result;
            }
            return null;
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("schoolAuthToken");
        }

        public async Task<bool> IsLoggedInAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("schoolAuthToken");
            return !string.IsNullOrEmpty(token);
        }

        public async Task<AuthViewModel> GetUserAsync(string userName)
        {
            var response = await _httpClient.GetFromJsonAsync<AuthViewModel>($"users/name/{userName}");
            return response;
        }
    }
}
