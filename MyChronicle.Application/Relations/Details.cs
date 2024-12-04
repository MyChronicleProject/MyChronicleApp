using MediatR;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;
using System;


namespace MyChronicle.Application.Relations
{
    public class Details
    {
        public class Query : IRequest<Result<Relation>>
        {
            public Guid Id { get; set; }
            public Guid PersonId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Relation>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Relation>> Handle(Query request, CancellationToken cancellationToken)
            {
                var relation = await _context.Relations.FindAsync(request.Id);
                if (relation == null) return Result<Relation>.Failure($"The Relation with Id {request.Id} could not be found", ErrorCategory.NotFound);

                if (request.PersonId != relation.PersonId_1 && request.PersonId != relation.PersonId_2)
                {
                    return Result<Relation>.Failure($"Not matching Id. Request PersonId was {request.PersonId}. Relation PersonId_1 was {relation.PersonId_1}. Relation PersonId_2 was {relation.PersonId_2}");
                }

                return Result<Relation>.Success(relation);
            }
        }

    }
}
