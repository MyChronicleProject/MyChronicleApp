namespace MyChronicle.Domain
{
    public enum FileType { Image, Audio, Document }
    public enum FileExtension { jpg, png, mp3, pdf, docx }
    public class File
    {
        public Guid Id { get; set; }
        public FileType FileType { get; set; }
        public Guid PersonId { get; set; }
        public byte[] Content { get; set; }
        public FileExtension FileExtension { get; set; }

        public virtual Person Person { get; set; }
    }
}
