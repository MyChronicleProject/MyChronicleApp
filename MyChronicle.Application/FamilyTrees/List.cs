using MediatR;
using Microsoft.EntityFrameworkCore;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;

namespace MyChronicle.Application.FamilyTrees
{
    public class List
    {
        public class Query : IRequest<Result<List<FamilyTree>>> { }

        public class Handler : IRequestHandler<Query, Result<List<FamilyTree>>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<FamilyTree>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.FamilyTrees.ToListAsync();
                return Result<List<FamilyTree>>.Success(result);
            }
        }
    }
}
