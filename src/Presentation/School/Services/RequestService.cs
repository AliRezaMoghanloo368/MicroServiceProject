using SharedLibrary.Patterns.ResultPattern;
using System.Text;

namespace School.Services
{
    public class RequestService
    {
        private HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        public RequestService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result<T>> GetAsync<T>(string routePath)
        {
            try
            {
                _httpClient = _httpClientFactory.CreateClient($"API/Request");
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_httpClient.BaseAddress + routePath),
                };

                var response = await _httpClient.SendAsync(request);
                // بررسی وضعیت پاسخ
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<Result<T>>();
                    return result;
                }
                else
                {
                    // در صورت عدم موفقیت، خطا را نشان بده
                    return new Result<T>
                    {
                        Success = false,
                        Message = $"Error: {response.StatusCode} - {response.ReasonPhrase}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<T>
                {
                    Success = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }

        public async Task<Result<bool>> PostAsync<T>(string routePath, object obj)
        {
            try
            {
                _httpClient = _httpClientFactory.CreateClient($"API/Request");
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_httpClient.BaseAddress + routePath),
                    Content = new StringContent(
                        System.Text.Json.JsonSerializer.Serialize(obj),
                        Encoding.UTF8,
                        "application/json")
                };

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return Result<bool>.SuccessResult(true);
                }
                else
                {
                    // در صورت عدم موفقیت، خطا را نشان بده
                    return new Result<bool>
                    {
                        Success = false,
                        Message = $"Error: {response.StatusCode} - {response.ReasonPhrase}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<bool>
                {
                    Success = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }

        public async Task<Result<bool>> PutAsync<T>(string routePath, object obj)
        {
            try
            {
                _httpClient = _httpClientFactory.CreateClient($"API/Request");
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(_httpClient.BaseAddress + routePath),
                    Content = new StringContent(
                        System.Text.Json.JsonSerializer.Serialize(obj),
                        Encoding.UTF8,
                        "application/json")
                };

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return Result<bool>.SuccessResult(true);
                }
                else
                {
                    // در صورت عدم موفقیت، خطا را نشان بده
                    return new Result<bool>
                    {
                        Success = false,
                        Message = $"Error: {response.StatusCode} - {response.ReasonPhrase}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<bool>
                {
                    Success = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }

        public async Task<Result<bool>> DeleteAsync<T>(string routePath, object obj)
        {
            try
            {
                _httpClient = _httpClientFactory.CreateClient($"API/Request");
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(_httpClient.BaseAddress + routePath),
                    Content = new StringContent(
                        System.Text.Json.JsonSerializer.Serialize(obj),
                        Encoding.UTF8,
                        "application/json")
                };

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return Result<bool>.SuccessResult(true);
                }
                else
                {
                    // در صورت عدم موفقیت، خطا را نشان بده
                    return new Result<bool>
                    {
                        Success = false,
                        Message = $"Error: {response.StatusCode} - {response.ReasonPhrase}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<bool>
                {
                    Success = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
    }
}
