using FluentValidation;

namespace MyChronicle.Application.FamilyTrees
{
    public class FamilyTreeDTOValidator : AbstractValidator<FamilyTreeDTO>
    {
        public FamilyTreeDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            //RuleFor(x => x.Layout).NotEmpty().WithMessage("Layout is required");
        }
    }
}
