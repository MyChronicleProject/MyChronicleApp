using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChronicle.Domain
{
    public enum Gender { Male, Female, NonBinary, Unspecified }
    public class Person
    {
        public Person()
        {
            this.RelationsAsPerson1 = new HashSet<Relations>();
            this.RelationsAsPerson2 = new HashSet<Relations>();
            this.Files              = new HashSet<Files>();
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

        public virtual ICollection<Relations> RelationsAsPerson1 { get; set; }
        public virtual ICollection<Relations> RelationsAsPerson2 { get; set; }
        public virtual ICollection<Files> Files {  get; set; }

    }
}
