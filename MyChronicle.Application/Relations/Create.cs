using FluentValidation;
using MediatR;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;

namespace MyChronicle.Application.Relations
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required Relation Relation {  get; set; }
        }

        public class  CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator() 
            {
                RuleFor(x => x.Relation).SetValidator(new RelationValidator());
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
                if (request.Relation.PersonId_1 == request.Relation.PersonId_2)
                {
                    return Result<Unit>.Failure("You cannot create a relationship between one person");
                }

                var person1 = await _context.Persons.FindAsync(request.Relation.PersonId_1);
                var person2 = await _context.Persons.FindAsync(request.Relation.PersonId_2);

                if (person1 == null) return Result<Unit>.Failure($"The Person with Id {request.Relation.PersonId_1} could not be found", ErrorCategory.NotFound);
                if (person2 == null) return Result<Unit>.Failure($"The Person with Id {request.Relation.PersonId_2} could not be found", ErrorCategory.NotFound);

                request.Relation.Person_1 = person1;
                request.Relation.Person_2 = person2;

                if (request.Relation.Person_1 == null || request.Relation.Person_2 == null)
                {
                    return Result<Unit>.Failure("PersonId_1 and PersonId_2 must be valid GUIDs.");
                }

                _context.Relations.Add(request.Relation);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create the Relation");
                return Result<Unit>.Success(Unit.Value);
            }
        }

      
    }
}
