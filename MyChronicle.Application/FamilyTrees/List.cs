using MediatR;
using Microsoft.EntityFrameworkCore;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;

namespace MyChronicle.Application.FamilyTrees
{
    public class List
    {
        public class Query : IRequest<Result<List<FamilyTree>>> { public string UserId { get; set; } }


        public class Handler : IRequestHandler<Query, Result<List<FamilyTree>>>
        {
            private readonly DataContext _context;
            

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<FamilyTree>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var loggedUserId = request.UserId;
                var result = await _context.FamilyTrees
                    .Include(ft => ft.FamilyTreePermisions)
                    .ThenInclude(ftp => ftp.AppUser)
                    .ToListAsync();


                foreach (var tree in result)
                {
               
                    var userPermission = tree.FamilyTreePermisions
                        .FirstOrDefault(ftp => ftp.AppUser.Id == loggedUserId);

                    tree.CurrentUserRole = userPermission?.Role ?? Role.Guest; 
                }

                return Result<List<FamilyTree>>.Success(result);
            }
        }
    }
}
