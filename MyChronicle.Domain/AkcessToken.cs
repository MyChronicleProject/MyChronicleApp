using System.ComponentModel.DataAnnotations.Schema;

namespace MyChronicle.Domain
{
    public class AkcessToken
    {
        public int          Id      { get; set; }
        public int          UserId  { get; set; }
        [Column(TypeName = "TEXT")]
        public string       Token   { get; set; }
        public DateTime     Expired { get; set; }
        public DateTime     Created { get; set; }
    }
}
