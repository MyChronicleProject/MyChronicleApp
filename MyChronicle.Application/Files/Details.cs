using MediatR;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;
using System;

namespace MyChronicle.Application.Files
{
    public class Details
    {
        public class Query : IRequest<Result<Domain.File>>
        {
            public Guid Id { get; set; }
            public Guid PersonId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Domain.File>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Domain.File>> Handle(Query request, CancellationToken cancellationToken)
            {
                var file = await _context.Files.FindAsync(request.Id);
                if (file == null) return Result<Domain.File>.Failure($"The File with Id {request.Id} could not be found", ErrorCategory.NotFound);

                if (file.PersonId != request.PersonId)
                {
                    return Result<Domain.File>.Failure($"Not matching Id. Request PersonId was {request.PersonId}. File PersonId was {file.PersonId}");
                }

                return Result<Domain.File>.Success(file);
            }
        }
    }
}
