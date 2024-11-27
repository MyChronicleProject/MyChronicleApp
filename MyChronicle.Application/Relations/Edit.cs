﻿using FluentValidation;
using MediatR;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;

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
                RuleFor(x => x.Relation).SetValidator(new RelationValidator());
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
                if (request.Relation.Id != request.Id)
                {
                    return Result<Unit>.Failure("Wrong Id");
                }

                var relation = await _context.Relations.FindAsync(request.Relation.Id);

                if (relation == null) return null;

                relation.RelationType = request.Relation.RelationType;
                relation.startDate = request.Relation.startDate ?? relation.startDate;
                relation.endDate = request.Relation.endDate ?? relation.endDate;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update Relation");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
