using System.ComponentModel.DataAnnotations.Schema;

namespace MyChronicle.Domain
{
    public class RefreshToken
    {
        public int          Id          { get; set; }
        public int          UserId      { get; set; }
        [Column(TypeName = "TEXT")]
        public string       Token       { get; set; }
        public DateTime     Expired     { get; set; }
        public DateTime     Created     { get; set; }    
        public Boolean      IsRevoked   {  get; set; }
        
    }
}
