using System.ComponentModel.DataAnnotations.Schema;

namespace MyChronicle.Domain
{
    public class FamilyTree
    {
        public Guid Id { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Name { get; set; }


        public ICollection<FamilyTreePermision> FamilyTreePermisions { get; set; } = [];
        public ICollection<Person> Persons { get; set; } = [];
    }
}
