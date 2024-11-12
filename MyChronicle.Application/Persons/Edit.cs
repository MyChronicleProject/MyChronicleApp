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
                if (request.Person.Id != request.Id)
                {
                    return Result<Unit>.Failure("Wrong Id");
                }

                var person = await _context.Persons.FindAsync(request.Person.Id);
                if (person == null) return null;

                if (person.FamilyTreeId != request.FamilyTreeId)
                {
                    return Result<Unit>.Failure("Bad FamilyTreeId");
                }

                person.Name = request.Person.Name ?? person.Name;
                person.MiddleName = request.Person.MiddleName ?? person.MiddleName;
                person.LastName = request.Person.LastName ?? person.LastName;
                person.BirthDate = request.Person.BirthDate;
                person.DeathDate = request.Person.DeathDate;
                person.BirthPlace = request.Person.BirthPlace ?? person.BirthPlace;
                person.DeathPlace = request.Person.DeathPlace ?? person.DeathPlace;
                person.Gender = request.Person.Gender;
                person.Occupation = request.Person.Occupation ?? person.Occupation;
                person.Note = request.Person.Note ?? person.Note;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update Person");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
