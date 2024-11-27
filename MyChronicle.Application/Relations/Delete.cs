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
                if (relation == null) return null;

                _context.Remove(relation);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete the relation");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
