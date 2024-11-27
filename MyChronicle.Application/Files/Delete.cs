using MediatR;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChronicle.Application.Files
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
                var file = await _context.Files.FindAsync(request.Id);

                if (file == null) return Result<Unit>.Failure($"The File with Id {request.Id} could not be found", ErrorCategory.NotFound);

                _context.Remove(file);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure($"Failed to delete the File, id: {file.Id}");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
