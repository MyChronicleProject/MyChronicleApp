namespace MyChronicle.Domain
{
    public enum Role {Autor, Guess}
    public class FamilyTreePermision
    {
        public int      Id              { get; set; }
        public int      FamilyTreeId    { get; set; }
        public int      UserId          { get; set; }
        public Role     Role            { get; set; }

        public virtual FamilyTree           FamilyTree  { get; set; }
        public virtual ICollection<User>   Users       { get; set; }

        public FamilyTreePermision()
        {
            this.Users = new HashSet<User>();  
        }
    }
}
