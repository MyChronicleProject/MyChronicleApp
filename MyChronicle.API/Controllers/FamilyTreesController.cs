using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyChronicle.Application;
using MyChronicle.Application.FamilyTrees;
using MyChronicle.Domain;

namespace MyChronicle.API.Controllers
{
    public class FamilyTreesController : BaseAPIController
    {
        private readonly IMediator _mediator;
        public FamilyTreesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetFamilyTrees()
        {
            var result = await _mediator.Send(new List.Query());

            if (result == null) return NotFound();
            if (result.IsSuccess && result.Value != null) return Ok(result.Value);
            if (result.IsSuccess && result.Value == null) return NotFound();
            return BadRequest();
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetFamilyTree(Guid id)
        {
            var result = await _mediator.Send(new Details.Query { Id = id });

            if (result == null) return NotFound();
            if (result.IsSuccess && result.Value != null) return Ok(result.Value);
            if (result.IsSuccess && result.Value == null) return NotFound();
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> PostFamilyTree(FamilyTree familyTree)
        {
            var result = await _mediator.Send(new Create.Command { FamilyTree = familyTree });
            
            if (result == null) return NotFound();
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFamilyTree(Guid id, FamilyTree familyTree)
        {
            if (id != familyTree.Id)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new Edit.Command { FamilyTree = familyTree });

            if (result == null) return NotFound();
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFamilyTree(Guid id)
        {
            var result = await _mediator.Send(new Delete.Command { Id = id });

            if (result == null) return NotFound();
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest();
        }
    }
}
