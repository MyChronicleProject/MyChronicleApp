using FluentValidation;
using MediatR;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;

namespace MyChronicle.Application.FamilyTrees
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required FamilyTreeDTO FamilyTreeDTO { get; set; }
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
                    Name = request.FamilyTreeDTO.Name
                };

                _context.FamilyTrees.Add(familyTree);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create FamilyTree");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
