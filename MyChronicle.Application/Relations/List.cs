using MediatR;
using Microsoft.EntityFrameworkCore;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;


namespace MyChronicle.Application.Relations
{
    public class List
    {
        public class Query : IRequest<Result<List<Relation>>>
        {
            public required Guid PersonId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<Relation>>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<Relation>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.Relations.Where(relation => relation.PersonId_1 == request.PersonId || relation.PersonId_2 == request.PersonId).ToListAsync();
                return Result<List<Relation>>.Success(result);
            }
        }
    }
}
