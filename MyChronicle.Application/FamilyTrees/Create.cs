using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;

namespace MyChronicle.Application.FamilyTrees
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required FamilyTreeDTO FamilyTreeDTO { get; set; }
            public required string OwnerId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator() {
                RuleFor(x => x.FamilyTreeDTO).SetValidator(new FamilyTreeDTOValidator());
            }
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
                var familyTree = new FamilyTree
                {
                    Id = request.FamilyTreeDTO.Id,
                    Name = request.FamilyTreeDTO.Name,
                    Layout = request.FamilyTreeDTO.Layout
                };

                _context.FamilyTrees.Add(familyTree);

                var user = await _context.Users.FirstAsync(user => user.Id == request.OwnerId);

                var familyTreePermission = new FamilyTreePermision
                {
                    AppUser = user,
                    FamilyTree = familyTree,
                    Role = Role.Author
                };

                _context.FamilyTreePermisions.Add(familyTreePermission);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create FamilyTree");
                return Result<Unit>.Success(Unit.Value);
            }

        }
    }
}
