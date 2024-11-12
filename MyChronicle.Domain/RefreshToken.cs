using System.ComponentModel.DataAnnotations.Schema;

namespace MyChronicle.Domain
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [Column(TypeName = "TEXT")]
        public string Token { get; set; }
        public DateTime Expired { get; set; }
        public DateTime Created { get; set; }
        public Boolean IsRevoked { get; set; }

    }
}
