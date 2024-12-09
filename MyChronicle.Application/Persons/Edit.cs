using FluentValidation;
using MediatR;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;

namespace MyChronicle.Application.Persons
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required Guid FamilyTreeId { get; set; }
            public required Guid Id { get; set; }
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
                if (request.PersonDTO.Id != request.Id)
                {
                    return Result<Unit>.Failure($"Not matching Id. Request Id was {request.Id}. Person Id was {request.PersonDTO.Id}");
                }

                if (request.PersonDTO.FamilyTreeId != request.FamilyTreeId)
                {
                    return Result<Unit>.Failure($"Not matching Id. Request FamilyTreeId was {request.FamilyTreeId}. Person FamilyTreeId was {request.PersonDTO.FamilyTreeId}");
                }

                var person = await _context.Persons.FindAsync(request.Id);
                if (person == null) return Result<Unit>.Failure($"The Person with Id {request.Id} could not be found", ErrorCategory.NotFound);

                person.Name = request.PersonDTO.Name ?? person.Name;
                person.MiddleName = request.PersonDTO.MiddleName ?? person.MiddleName;
                person.LastName = request.PersonDTO.LastName ?? person.LastName;
                person.BirthDate = request.PersonDTO.BirthDate;
                person.DeathDate = request.PersonDTO.DeathDate;
                person.BirthPlace = request.PersonDTO.BirthPlace ?? person.BirthPlace;
                person.DeathPlace = request.PersonDTO.DeathPlace ?? person.DeathPlace;
                person.Gender = request.PersonDTO.Gender;
                person.Occupation = request.PersonDTO.Occupation ?? person.Occupation;
                person.Note = request.PersonDTO.Note ?? person.Note;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update Person");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
