namespace Files.Domain.Models
{
    public class FilesEntity
    {
        public FilesEntity()
        {
            Id = Guid.NewGuid();
            UploadAt = DateTime.Now;
        }
        public Guid Id { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        //public string? FileName { get; set; }
        //public string? FileType { get; set; }
        //public long? FileSize { get; set; }
        public byte[]? FileContent { get; set; }
        //public string? FilePath { get; set; }
        //public string? FileDate { get; set; }
        //public string? Description { get; set; }
        //public string? UploadBy { get; set; }
        public DateTime UploadAt { get; set; }
        //public bool IsActive { get; set; }
    }
}
