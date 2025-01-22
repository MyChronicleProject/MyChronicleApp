using System.Text.Json.Serialization;

namespace MyChronicle.Domain
{
    public enum Role { Author, Guest }
    public class FamilyTreePermision
    {
        public Guid Id { get; set; }
        public Guid FamilyTreeId { get; set; }
        public Role Role { get; set; }

        [JsonIgnore]
        public FamilyTree FamilyTree { get; set; } = null!;
        [JsonIgnore]
        public AppUser AppUser { get; set; } = null!;
    }
}
