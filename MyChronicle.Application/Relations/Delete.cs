using MediatR;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChronicle.Application.Relations
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required Guid Id { get; set; }
            public required Guid PersonId { get; set; }
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
                var relation = await _context.Relations.FindAsync(request.Id);
                if (relation == null) return Result<Unit>.Failure($"The Relation with Id {request.Id} could not be found", ErrorCategory.NotFound);

                if (request.PersonId != relation.PersonId_1 && request.PersonId != relation.PersonId_2)
                {
                    return Result<Unit>.Failure($"Not matching Id. Request PersonId was {request.PersonId}. Relation PersonId_1 was {relation.PersonId_1}. Relation PersonId_2 was {relation.PersonId_2}");
                }

                _context.Remove(relation);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete the relation");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
