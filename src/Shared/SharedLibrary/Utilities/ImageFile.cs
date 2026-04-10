namespace SharedLibrary.Utilities
{
    public class ImageFile
    {
        public static byte[]? Buffer { get; set; } //copy the stream to the buffer
        public string? base64data { get; set; }
        public string? contentType { get; set; }
        public string? fileName { get; set; }
    }
}
