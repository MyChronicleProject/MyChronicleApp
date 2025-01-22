using Microsoft.AspNetCore.Identity;

namespace MyChronicle.Domain
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public ICollection<FamilyTreePermision> FamilyTreesPermisions { get; set; } = [];
    }
}
