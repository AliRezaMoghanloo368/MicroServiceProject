namespace Files.Application.Dtos
{
    public class UpdateFileDto
    {
        public string Id { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public byte[]? FileContent { get; set; }
    }
}
