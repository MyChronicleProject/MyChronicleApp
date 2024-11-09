using System.ComponentModel.DataAnnotations.Schema;

namespace MyChronicle.Domain
{
    public class FamilyTree
    {
        public FamilyTree()
        {
            this.FamilyTreePermision    = new HashSet<FamilyTreePermision>();
            this.Person                 = new HashSet<Person>();
        }

        public int      Id   { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string   Name { get; set; }


        public virtual ICollection<FamilyTreePermision> FamilyTreePermision { get; set; }
        public virtual ICollection<Person>              Person              { get; set; }
    }
}
