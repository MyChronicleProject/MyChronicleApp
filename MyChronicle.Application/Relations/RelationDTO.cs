using MyChronicle.Domain;

namespace MyChronicle.Application.Relations
{
    public class RelationDTO
    {
        public Guid Id { get; set; }
        public Guid PersonId_1 { get; set; }
        public Guid PersonId_2 { get; set; }
        public RelationType RelationType { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
