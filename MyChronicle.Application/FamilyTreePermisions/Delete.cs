using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChronicle.Application.FamilyTreePermisions
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required Guid Id { get; set; }
            public required string AppUser { get; set; }
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
                var familyTreePermision = await _context.FamilyTreePermisions
                        .FirstOrDefaultAsync(ft => ft.FamilyTreeId == request.Id && ft.AppUser.Id == request.AppUser);

                if (familyTreePermision == null) return Result<Unit>.Failure($"The FamilyTree with Id {request.Id} could not be found", ErrorCategory.NotFound);

                _context.Remove(familyTreePermision);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure($"Failed to delete the FamilyTreePermision, id: {familyTreePermision.Id}");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
