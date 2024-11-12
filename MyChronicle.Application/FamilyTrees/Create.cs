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
            public required FamilyTree FamilyTree { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator() {
                RuleFor(x => x.FamilyTree).SetValidator(new FamilyTreeValidator());
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
                _context.FamilyTrees.Add(request.FamilyTree);
                var result = await _context.SaveChangesAsync() > 0;

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
