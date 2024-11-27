using FluentValidation;
using MyChronicle.Domain;

namespace MyChronicle.Application.Relations
{
    public class RelationValidator : AbstractValidator<Relation>
    {
        public RelationValidator() 
        {
            RuleFor(x => x.RelationType).IsInEnum().WithMessage("Invalid relation type.");
            RuleFor(x => x.PersonId_1).NotEmpty().WithMessage("PersonId_1 is required.");
            RuleFor(x => x.PersonId_2).NotEmpty().WithMessage("PersonId_2 is required.");
            RuleFor(x => x.startDate).NotEmpty().WithMessage("Start date is required.");
            RuleFor(x => x.Person_1).Null(); 
            RuleFor(x => x.Person_2).Null();
        }
    }
}
