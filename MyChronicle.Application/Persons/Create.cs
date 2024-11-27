using FluentValidation;
using MediatR;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;

namespace MyChronicle.Application.Persons
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required Guid FamilyTreeId { get; set; }
            public required Person Person { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Person).SetValidator(new PersonValidator());
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
                if (request.Person.FamilyTreeId != request.FamilyTreeId)
                {
                    return Result<Unit>.Failure($"Not matching Id. Request FamilyTreeId was {request.FamilyTreeId}. Request Person.FamilyTreeId was {request.Person.FamilyTreeId}");
                }

                _context.Persons.Add(request.Person);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create Person");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
