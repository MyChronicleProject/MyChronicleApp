using MediatR;
using Microsoft.EntityFrameworkCore;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;

namespace MyChronicle.Application.FamilyTrees
{
    public class Details
    {
        public class Query : IRequest<Result<FamilyTree>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<FamilyTree>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<FamilyTree>> Handle(Query request, CancellationToken cancellationToken)
            {
                var familyTree = await _context.FamilyTrees
                    .Include(ft => ft.Persons)
                    .FirstAsync(ft => ft.Id == request.Id);
                if (familyTree == null) return Result<FamilyTree>.Failure($"The FamilyTree with Id {request.Id} could not be found", ErrorCategory.NotFound);

                return Result<FamilyTree>.Success(familyTree);
            }
        }
    }
}
