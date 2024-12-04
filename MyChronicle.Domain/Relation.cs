using System.Text.Json.Serialization;

namespace MyChronicle.Domain
{
    public enum RelationType { Child, Parent, Spouse }
    public class Relation
    {
        public Guid Id { get; set; }
        public Guid PersonId_1 { get; set; }
        public Guid PersonId_2 { get; set; }
        public RelationType RelationType { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        [JsonIgnore]
        public Person Person_1 { get; set; } = null!;
        [JsonIgnore]
        public Person Person_2 { get; set; } = null!;
    }
}
