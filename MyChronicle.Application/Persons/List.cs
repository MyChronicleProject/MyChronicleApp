using MediatR;
using Microsoft.EntityFrameworkCore;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;

namespace MyChronicle.Application.Persons
{
    public class List
    {
        public class Query : IRequest<Result<List<Person>>>
        {
            public required Guid FamilyTreeId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<Person>>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<Person>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.Persons.Where(person => person.FamilyTreeId == request.FamilyTreeId).ToListAsync();
                return Result<List<Person>>.Success(result);
            }
        }
    }
}
