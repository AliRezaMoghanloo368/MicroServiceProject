using School.ViewModels;
using SharedLibrary.Patterns.ResultPattern;

namespace School.Services
{
    public class FilesService
    {
        private readonly HttpClient _httpClient;
        public FilesService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API/Files");
        }

        public async Task<Result<bool>> UploadAsync(FileViewModel createFile)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Files/upload", createFile);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Result<bool>>();
                return result;
            }
            return null;
        }

        public async Task<Result<List<FileViewModel>>> LoadAsync(FileViewModel file)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Files/load", file);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Result<List<FileViewModel>>>();
                return result;
            }
            return null;
        }
    }
}
