using Microsoft.AspNetCore.Http;

namespace Files.Application.Dtos
{
    public class UpdateFileDto
    {
        public string Id { get; set; } = null!;
        public string EntityName { get; set; } = null!;
        public string EntityId { get; set; } = null!;
        public IFormFile? FileContent { get; set; } = null!;
    }
}
