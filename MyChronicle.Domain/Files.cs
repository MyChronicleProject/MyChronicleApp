using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MyChronicle.Domain
{
    public enum FileType        { Image, Audio, Document }
    public enum FileExtension   { jpg, png, mp3, pdf, docx }
    public class Files
    {
        public int              Id              { get; set; }
        public FileType         FileType        { get; set; }
        public int              PersonId        {  get; set; }
        public byte[]           File            {  get; set; }
        public FileExtension    FileExtension   { get; set; }

        public virtual Person Person { get; set; }
    }
}
