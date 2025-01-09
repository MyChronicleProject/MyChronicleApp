using Microsoft.AspNetCore.Identity;

namespace MyChronicle.Domain
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get => FirstName + " " + LastName; }
    }
}
