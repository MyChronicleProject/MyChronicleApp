using FluentValidation;
using MyChronicle.Domain;

namespace MyChronicle.Application.Files
{
    public class FileValidator : AbstractValidator<MyChronicle.Domain.File>
    {
        public FileValidator()
        {
            RuleFor(x => x.FileType).IsInEnum().WithMessage("Invalid file type.");
            RuleFor(x => x.PersonId).NotEmpty().WithMessage("PersonId is required.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
            RuleFor(x => x.FileExtension).IsInEnum().WithMessage("Invalid file extenion");
        }
    }
}
