using MediatR;
using Microsoft.EntityFrameworkCore;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;

namespace MyChronicle.Application.FamilyTrees
{
    public class Share
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required Guid FamilyTreeId { get; set; }
            public required string UserId { get; set; }
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
                var familyTree = await _context.FamilyTrees.FirstOrDefaultAsync(ft => ft.Id == request.FamilyTreeId);

                if (familyTree == null)
                {
                    return Result<Unit>.Failure($"Failed to find FamilyTree with Id {request.FamilyTreeId}", category: ErrorCategory.NotFound);
                }

                var user = await _context.Users.FirstAsync(user => user.Id == request.UserId);

                var familyTreePermission = new FamilyTreePermision
                {
                    AppUser = user,
                    FamilyTree = familyTree,
                    Role = Role.Guest
                };

                _context.FamilyTreePermisions.Add(familyTreePermission);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create FamilyTreePermission");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
