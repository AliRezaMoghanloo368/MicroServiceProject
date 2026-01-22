using Microsoft.AspNetCore.Http;

namespace Files.Application.Dtos
{
    public class CreateFileDto
    {
        public string EntityName { get; set; } = null!;
        public string EntityId { get; set; } = null!;
        public IFormFile? FileContent { get; set; } = null!;
    }
}
