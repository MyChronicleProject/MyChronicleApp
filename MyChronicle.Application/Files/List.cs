using MediatR;
using Microsoft.EntityFrameworkCore;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;
using System;

namespace MyChronicle.Application.Files
{
    public class List
    {
        public class Query : IRequest<Result<List<MyChronicle.Domain.File>>>
        {
            public required Guid PersonId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<MyChronicle.Domain.File>>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<MyChronicle.Domain.File>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var person = await _context.Persons.FindAsync(request.PersonId);

                if (person == null)
                {
                    return Result<List<Domain.File>>.Failure($"The Person with Id {request.PersonId} could not be found", ErrorCategory.NotFound);
                }

                var result = await _context.Files.Where(file => file.PersonId == request.PersonId).ToListAsync();
                return Result<List<Domain.File>>.Success(result);
            }
        }
    }
}
