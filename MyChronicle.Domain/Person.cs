using System.ComponentModel.DataAnnotations.Schema;

namespace MyChronicle.Domain
{
    public enum Gender { Male, Female, NonBinary, Unspecified }
    public class Person
    {
        public Person()
        {
            this.RelationsAsPerson1 = new HashSet<Relation>();
            this.RelationsAsPerson2 = new HashSet<Relation>();
            this.Files              = new HashSet<File>();
        }
        public int          Id              { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string       Name            { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string       MiddleName      { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string       LastName        { get; set; }

        public DateOnly     BirthDate       {  get; set; }
        public DateOnly?    DeathDate       { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string       BirthPlace      { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string?      DeathPlace      { get; set; }
        public Gender       Gender          { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string?      Occupation      {  get; set; }
        [Column(TypeName = "LONGTEXT")]
        public string?      Note            { get; set; }
        public int          FamilyTreeId    {  get; set; }

        public virtual ICollection<Relation> RelationsAsPerson1 { get; set; }
        public virtual ICollection<Relation> RelationsAsPerson2 { get; set; }
        public virtual ICollection<File> Files {  get; set; }

    }
}
