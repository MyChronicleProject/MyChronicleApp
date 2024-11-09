using MediatR;
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
                var familyTree = await _context.FamilyTrees.FindAsync(request.Id);
                return Result<FamilyTree>.Success(familyTree);
            }
        }
    }
}
