using FluentValidation;

namespace MyChronicle.Application.Relations
{
    public class RelationDTOValidator : AbstractValidator<RelationDTO>
    {
        public RelationDTOValidator() 
        {
            RuleFor(x => x.RelationType).IsInEnum().WithMessage("Invalid relation type.");
            RuleFor(x => x.PersonId_1).NotEmpty().WithMessage("PersonId_1 is required.");
            RuleFor(x => x.PersonId_2).NotEmpty().WithMessage("PersonId_2 is required.");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date is required.");
        }
    }
}
