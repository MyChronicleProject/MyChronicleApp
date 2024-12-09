using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyChronicle.Domain
{
    public class AccessToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [Column(TypeName = "TEXT")]
        public string Token { get; set; }
        public DateTime Expired { get; set; }
        public DateTime Created { get; set; }

        [JsonIgnore]
        public User User { get; set; } = null!;
    }
}
