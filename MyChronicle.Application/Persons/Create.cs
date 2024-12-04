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
            public required PersonDTO PersonDTO { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.PersonDTO).SetValidator(new PersonDTOValidator());
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
                if (request.PersonDTO.FamilyTreeId != request.FamilyTreeId)
                {
                    return Result<Unit>.Failure($"Not matching Id. Request FamilyTreeId was {request.FamilyTreeId}. Request Person.FamilyTreeId was {request.PersonDTO.FamilyTreeId}");
                }

                var familyTree = await _context.FamilyTrees.FindAsync(request.FamilyTreeId);
                if (familyTree == null) return Result<Unit>.Failure($"The FamilyTree with Id {request.FamilyTreeId} could not be found", ErrorCategory.NotFound);

                var person = new Person
                {
                    Id = request.PersonDTO.Id,
                    Name = request.PersonDTO.Name,
                    MiddleName = request.PersonDTO.MiddleName,
                    LastName = request.PersonDTO.LastName,
                    BirthDate = request.PersonDTO.BirthDate,
                    DeathDate = request.PersonDTO.DeathDate,
                    BirthPlace = request.PersonDTO.BirthPlace,
                    DeathPlace = request.PersonDTO.DeathPlace,
                    Gender = request.PersonDTO.Gender,
                    Occupation = request.PersonDTO.Occupation,
                    Note = request.PersonDTO.Note,
                    FamilyTreeId = request.FamilyTreeId,
                    FamilyTree = familyTree
                };

                _context.Persons.Add(person);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create Person");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
