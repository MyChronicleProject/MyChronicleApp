using FluentValidation;
using MediatR;
using MyChronicle.Infrastructure;

namespace MyChronicle.Application.FamilyTrees
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
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
                var familyTree = await _context.FamilyTrees.FindAsync(request.Id);
                if (familyTree == null) return Result<Unit>.Failure($"The FamilyTree with Id {request.Id} could not be found", ErrorCategory.NotFound);

                _context.Remove(familyTree);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure($"Failed to delete the FamilyTree, id: {familyTree.Id}");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
