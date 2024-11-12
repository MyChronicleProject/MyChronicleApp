using FluentValidation;
using MyChronicle.Domain;

namespace MyChronicle.Application.FamilyTrees
{
    public class FamilyTreeValidator : AbstractValidator<FamilyTree>
    {
        public FamilyTreeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
