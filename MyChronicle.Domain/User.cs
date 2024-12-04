using System.ComponentModel.DataAnnotations.Schema;

namespace MyChronicle.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string Login { get; set; }
        [Column(TypeName = "TEXT")]
        public string PasswordHash { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string FirstName { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string LastName { get; set; }

        public ICollection<AccessToken> AkcessTokens { get; set; } = [];
        public ICollection<RefreshToken> RefreshToken { get; set; } = [];
    }
}
