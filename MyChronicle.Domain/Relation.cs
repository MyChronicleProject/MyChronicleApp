namespace MyChronicle.Domain
{
    public enum RelationType {Parent, Child, Sibling, Spouse, Uncle, Aunt, Cousin, Partner }
    public class Relation
    {
        public int          Id              { get; set; }
        public int          PersonId_1      { get; set; }
        public int          PersonId_2      { get; set; }
        public RelationType RelationType    { get; set; }
        public DateOnly     startDate       { get; set; }
        public DateOnly     endDate         { get; set; }

        public virtual Person Person_1      { get; set; }
        public virtual Person Person_2      { get; set; }
    }
}
