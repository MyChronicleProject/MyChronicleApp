using FluentValidation;

namespace MyChronicle.Application.Persons
{
    public class PersonDTOValidator : AbstractValidator<PersonDTO>
    {
        public PersonDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required");
            RuleFor(x => x.BirthDate).NotEmpty().WithMessage("BirthDate is required");
            RuleFor(x => x.FamilyTreeId).NotEmpty().WithMessage("FamilyTreeId is required");
        }
    }
}
