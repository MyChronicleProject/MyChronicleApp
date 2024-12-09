using MediatR;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MyChronicle.Application.Relations
{
    public class ListForOneTree
    {
        public class Query : IRequest<Result<List<Relation>>>
        {
            public required Guid FamilyTreeId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<Relation>>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<Relation>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var relations = await _context.Relations.ToListAsync(cancellationToken);

                var validRelations = new List<Relation>();

                foreach (var relation in relations)
                {
                    var person1 = await _context.Persons
                        .FirstOrDefaultAsync(person => person.Id == relation.PersonId_1, cancellationToken);

                    if (person1 != null && person1.FamilyTreeId == request.FamilyTreeId)
                    {
                        validRelations.Add(relation);
                    }
                }


                return Result<List<Relation>>.Success(validRelations);
            }
        }
    }

}
