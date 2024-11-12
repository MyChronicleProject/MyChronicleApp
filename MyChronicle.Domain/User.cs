using System.ComponentModel.DataAnnotations.Schema;

namespace MyChronicle.Domain
{
    public class User
    {
        public User()
        {
            this.AkcessTokens = new HashSet<AkcessToken>();
            this.RefreshToken = new HashSet<RefreshToken>();
        }
        public Guid Id { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string Login { get; set; }
        [Column(TypeName = "TEXT")]
        public string PasswordHash { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string FirstName { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string LastName { get; set; }

        public virtual ICollection<AkcessToken> AkcessTokens { get; set; }
        public virtual ICollection<RefreshToken> RefreshToken { get; set; }
    }
}
