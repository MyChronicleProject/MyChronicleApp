using FluentValidation;
using MediatR;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;
using System;

namespace MyChronicle.Application.Relations
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required Guid Id { get; set; }
            public required Relation Relation { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                //RuleFor(x => x.Relation).SetValidator(new RelationDTOValidator());
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
                var relation = await _context.Relations.FindAsync(request.Relation.Id);

                if (relation == null) return Result<Unit>.Failure($"The Relation with Id {request.Relation.Id} could not be found", ErrorCategory.NotFound);

                if (relation.Id != request.Id)
                {
                    return Result<Unit>.Failure($"Not matching Id. Request Id was {request.Id}. Relation Id was {relation.Id}");
                }

                relation.RelationType = request.Relation.RelationType;
                relation.StartDate = request.Relation.StartDate ?? relation.StartDate;
                relation.EndDate = request.Relation.EndDate ?? relation.EndDate;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update Relation");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}

