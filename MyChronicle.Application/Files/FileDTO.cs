using MyChronicle.Domain;

namespace MyChronicle.Application.Files
{
    public class FileDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public FileType FileType { get; set; }
        public byte[] Content { get; set; }
        public FileExtension FileExtension { get; set; }
        public Guid PersonId { get; set; }
    }
}
