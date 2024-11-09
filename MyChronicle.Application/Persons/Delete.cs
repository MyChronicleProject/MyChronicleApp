using MediatR;
using MyChronicle.Infrastructure;

namespace MyChronicle.Application.Persons
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required Guid FamilyTreeId { get; set; }
            public required Guid Id { get; set; }
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
                var person = await _context.Persons.FindAsync(request.Id);

                if (person.FamilyTreeId != request.FamilyTreeId)
                {
                    return Result<Unit>.Failure("Bad FamilyTreeId");
                }

                _context.Remove(person);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete the familyTree");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
