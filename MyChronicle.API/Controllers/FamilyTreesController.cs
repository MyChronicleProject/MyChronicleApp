using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;

namespace MyChronicle.API.Controllers
{
    public class FamilyTreesController : BaseAPIController
    {
        private readonly DataContext _context;
        public FamilyTreesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<FamilyTree>>> GetFamilyTrees()
        {
            return await _context.FamilyTrees.ToListAsync();
        }
        
        [HttpGet("id")]
        public async Task<ActionResult<FamilyTree>> GetFamilyTree(int id)
        {
            return await _context.FamilyTrees.FindAsync(id);
        }
    }
}
