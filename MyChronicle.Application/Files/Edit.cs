using FluentValidation;
using MediatR;
using MyChronicle.Infrastructure;

namespace MyChronicle.Application.Files
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required Guid Id { get; set; }
            public required Guid PersonId { get; set; }
            public required FileDTO FileDTO { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.FileDTO).SetValidator(new FileDTOValidator());
            }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.FileDTO.Id != request.Id)
                {
                    return Result<Unit>.Failure($"Not matching Id. Request Id was {request.Id}. File id was {request.FileDTO.Id}");
                }

                var file = await _context.Files.FindAsync(request.Id);
                if (file == null) return Result<Unit>.Failure($"The File with Id {request.Id} could not be found", ErrorCategory.NotFound);

                file.Content = request.FileDTO.Content;
                file.FileType = request.FileDTO.FileType;
                file.FileExtension = request.FileDTO.FileExtension;
                file.Name = request.FileDTO.Name;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update File");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
