using MediatR;
using Microsoft.EntityFrameworkCore;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;

namespace MyChronicle.Application.Persons
{
    public class Details
    {
        public class Query : IRequest<Result<Person>>
        {
            public Guid FamilyTreeId { get; set; }
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Person>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<Person>> Handle(Query request, CancellationToken cancellationToken)
            {
                var person = await _context.Persons
                    .Include(p => p.RelationsAsPerson1)
                    .Include(p => p.RelationsAsPerson2)
                    .Include(p => p.Files)
                    .FirstAsync(p => p.Id == request.Id);
                if (person == null) return Result<Person>.Failure($"The Person with Id {request.Id} could not be found", ErrorCategory.NotFound);

                if (person.FamilyTreeId != request.FamilyTreeId)
                {
                    return Result<Person>.Failure($"Not matching Id. Request FamilyTreeId was {request.FamilyTreeId}. Person FamilyTreeId was {person.FamilyTreeId}");
                }
                return Result<Person>.Success(person);
            }
        }
    }
}
