namespace School.ViewModels
{
    public class FileViewModel
    {
        public string Id { get; set; } = null!;
        public string EntityName { get; set; } = null!;
        public string EntityId { get; set; } = null!;
        public byte[]? FileContent { get; set; } = null!;
    }
}
