namespace Files.Application.Dtos
{
    public class CreateFileDto
    {
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public byte[]? FileContent { get; set; }
    }
}
