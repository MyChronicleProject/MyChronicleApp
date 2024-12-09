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
            public required Guid PersonId { get; set; }
            public required RelationDTO RelationDTO { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.RelationDTO).SetValidator(new RelationDTOValidator());
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
                if (request.RelationDTO.Id != request.Id)
                {
                    return Result<Unit>.Failure($"Not matching Id. Request Id was {request.Id}. Relation id was {request.RelationDTO.Id}");
                }

                var relation = await _context.Relations.FindAsync(request.RelationDTO.Id);
                if (relation == null) return Result<Unit>.Failure($"The Relation with Id {request.RelationDTO.Id} could not be found", ErrorCategory.NotFound);

                relation.RelationType = request.RelationDTO.RelationType;
                relation.StartDate = request.RelationDTO.StartDate ?? relation.StartDate;
                relation.EndDate = request.RelationDTO.EndDate ?? relation.EndDate;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update Relation");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}

