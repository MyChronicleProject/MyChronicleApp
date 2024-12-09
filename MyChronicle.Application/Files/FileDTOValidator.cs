using FluentValidation;

namespace MyChronicle.Application.Files
{
    public class FileDTOValidator : AbstractValidator<FileDTO>
    {
        public FileDTOValidator()
        {
            RuleFor(x => x.FileType).IsInEnum().WithMessage("Invalid file type.");
            RuleFor(x => x.PersonId).NotEmpty().WithMessage("PersonId is required.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
            RuleFor(x => x.FileExtension).IsInEnum().WithMessage("Invalid file extenion");
        }
    }
}
