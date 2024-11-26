﻿using MediatR;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;


namespace MyChronicle.Application.Relations
{
    public class Details
    {
        public class Query : IRequest<Result<Relation>>
        {
            public Guid Id { get; set; }
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
                var result = await _context.Relations.FindAsync(request.Id);
                if (result == null) return Result<Relation>.Failure($"The Relation with Id {request.Id} could not be found", ErrorCategory.NotFound);

                return Result<Relation>.Success(result);
            }
        }

    }
}
