using FluentValidation;
using MediatR;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;
using System;

namespace MyChronicle.Application.Relations
{
    public class Create
    {
        public class Command : IRequest<Result<Guid>>
        {
            public required Guid PersonId { get; set; }
            public required RelationDTO RelationDTO {  get; set; }
        }

        public class  CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator() 
            {
                RuleFor(x => x.RelationDTO).SetValidator(new RelationDTOValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Guid>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.RelationDTO.PersonId_1 != request.PersonId && request.RelationDTO.PersonId_2 != request.PersonId)
                {
                    return Result<Guid>.Failure($"Not matching Id. Request PeronsId was {request.PersonId}. Request RelationId.PersonId_1 was {request.RelationDTO.PersonId_1}. Request RelationId.PersonId_2 was {request.RelationDTO.PersonId_2}.");
                }

                if (request.RelationDTO.PersonId_1 == request.RelationDTO.PersonId_2)
                {
                    return Result<Guid>.Failure("You cannot create a relationship between one person");
                }

                var person1 = await _context.Persons.FindAsync(request.RelationDTO.PersonId_1);
                var person2 = await _context.Persons.FindAsync(request.RelationDTO.PersonId_2);

                if (person1 == null) return Result<Guid>.Failure($"The Person with Id {request.RelationDTO.PersonId_1} could not be found", ErrorCategory.NotFound);
                if (person2 == null) return Result<Guid>.Failure($"The Person with Id {request.RelationDTO.PersonId_2} could not be found", ErrorCategory.NotFound);

                var relation = new Relation
                {
                    Id = request.RelationDTO.Id,
                    RelationType = request.RelationDTO.RelationType,
                    StartDate = request.RelationDTO.StartDate,
                    EndDate = request.RelationDTO.EndDate,
                    PersonId_1 = request.RelationDTO.PersonId_1,
                    PersonId_2 = request.RelationDTO.PersonId_2,
                    Person_1 = person1,
                    Person_2 = person2
                };

                _context.Relations.Add(relation);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Guid>.Failure("Failed to create the Relation");
                return Result<Guid>.Success(relation.Id);
            }
        }

      
    }
}
