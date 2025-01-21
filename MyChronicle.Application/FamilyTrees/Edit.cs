using FluentValidation;
using MediatR;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;

namespace MyChronicle.Application.FamilyTrees
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required Guid Id { get; set; }
            public required FamilyTree FamilyTree { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                //RuleFor(x => x.FamilyTree).SetValidator(new FamilyTreeDTOValidator());
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
                var familyTree = await _context.FamilyTrees.FindAsync(request.FamilyTree.Id);
                if (familyTree == null) return Result<Unit>.Failure($"The FamilyTree with Id {request.Id} could not be found", ErrorCategory.NotFound);

                if (familyTree.Id != request.Id)
                {
                    return Result<Unit>.Failure($"Not matching Id. Request Id was {request.Id}. FamilyTree id was {familyTree.Id}");
                }

                familyTree.Name = request.FamilyTree.Name ?? familyTree.Name;
                familyTree.Layout = request.FamilyTree.Layout ?? familyTree.Layout;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update familyTree");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
