namespace MyChronicle.Domain
{
    public enum RelationType { Child, Parent, Spouse }
    public class Relation
    {
        public Guid Id { get; set; }
        public Guid PersonId_1 { get; set; }
        public Guid PersonId_2 { get; set; }
        public RelationType RelationType { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }

        public virtual Person Person_1 { get; set; }
        public virtual Person Person_2 { get; set; }
    }
}
