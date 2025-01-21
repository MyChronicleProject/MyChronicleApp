using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyChronicle.Domain
{
    public class FamilyTree
    {
        public Guid Id { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Name { get; set; }
        public byte[] Layout { get; set; }

        [JsonIgnore]
        public ICollection<FamilyTreePermision> FamilyTreePermisions { get; set; } = [];
        public ICollection<Person> Persons { get; set; } = [];
    }
}
