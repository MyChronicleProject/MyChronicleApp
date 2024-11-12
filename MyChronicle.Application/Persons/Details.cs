using MediatR;
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
                var person = await _context.Persons.FindAsync(request.Id);

                if (person.FamilyTreeId != request.FamilyTreeId) return Result<Person>.Failure("Bad FamilyTreeId");
                return Result<Person>.Success(person);
            }
        }
    }
}
