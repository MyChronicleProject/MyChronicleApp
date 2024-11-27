using MediatR;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;

namespace MyChronicle.Application.Files
{
    public class Details
    {
        public class Query : IRequest<Result<MyChronicle.Domain.File>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<MyChronicle.Domain.File>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Domain.File>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.Files.FindAsync(request.Id);

                if (result == null)
                {
                    //return Result<MyChronicle.Domain.File>.Failure("No file with the given id was found");
                    return null;
                }

                return Result<Domain.File>.Success(result);
            }
        }
    }
}
