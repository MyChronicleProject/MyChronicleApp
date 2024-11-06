using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChronicle.Domain
{
    public class Users
    {
        public Users() 
        {
            this.AkcessTokens = new HashSet<AkcessToken>();
            this.RefreshToken = new HashSet<RefreshToken>();
        }
        public int      Id              { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string   Login           { get; set; }
        [Column(TypeName = "TEXT")]
        public string   PasswordHash    { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string   FirstName       { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string   LastName        { get; set; }

        public virtual ICollection<AkcessToken>     AkcessTokens { get; set; }
        public virtual ICollection<RefreshToken>    RefreshToken { get; set; }
    }
}
