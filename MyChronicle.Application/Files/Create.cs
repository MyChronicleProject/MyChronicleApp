using FluentValidation;
using MediatR;
using MyChronicle.Application.Relations;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;

namespace MyChronicle.Application.Files
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required MyChronicle.Domain.File File { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.File).SetValidator(new FileValidator());
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
                var person = await _context.Persons.FindAsync(request.File.PersonId);

                if (person == null) return Result<Unit>.Failure($"The Person with Id {person.Id} could not be found", ErrorCategory.NotFound);
                request.File.Person = person;

                _context.Files.Add(request.File);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create File"); 
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
