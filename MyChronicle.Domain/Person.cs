using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace MyChronicle.Domain
{
    public enum Gender { Male, Female, NonBinary, Unspecified }
    public class Person
    {
        public Guid Id { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string Name { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string? MiddleName { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string LastName { get; set; }

        public DateOnly BirthDate { get; set; }
        public DateOnly? DeathDate { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string? BirthPlace { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string? DeathPlace { get; set; }
        public Gender? Gender { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string? Occupation { get; set; }
        [Column(TypeName = "LONGTEXT")]
        public string? Note { get; set; }
        public Guid FamilyTreeId { get; set; }

        [JsonIgnore]
        public FamilyTree FamilyTree { get; set; } = null!;

        public ICollection<Relation> RelationsAsPerson1 { get; set; } = [];
        public ICollection<Relation> RelationsAsPerson2 { get; set; } = [];
        public ICollection<File> Files { get; set; } = [];

    }
}
