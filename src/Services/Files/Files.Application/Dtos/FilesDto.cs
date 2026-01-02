namespace Files.Application.Dtos
{
    public class FilesDto
    {
        public Guid Id { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public byte[]? FileContent { get; set; }
        public DateTime UploadAt { get; set; }
    }
}
