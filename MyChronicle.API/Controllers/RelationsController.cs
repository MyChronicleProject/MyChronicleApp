using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyChronicle.Application.Relations;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;

namespace MyChronicle.API.Controllers
{
    [Route("api/FamilyTrees/{treeId}/Person/{personId}/[controller]")]
    public class RelationsController : BaseAPIController
    {
        private readonly IMediator _mediator;
        public RelationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetRelaitions(Guid treeId,Guid personId)
        {
            
            var result = await _mediator.Send(new List.Query { PersonId = personId });

            if (result == null) return NotFound();
            if (result.IsSuccess && result.Value != null) return Ok(result.Value);
            if (result.IsSuccess && result.Value == null) return NotFound();
            return BadRequest(result.ErrorMsg);
        }

        [HttpGet("{relationId}")]
        public async Task<IActionResult> GetRelation(Guid treeId, Guid personId,Guid relationId)
        {

            var result = await _mediator.Send(new Details.Query { Id = relationId});

            if (result == null) return NotFound();
            if (result.IsSuccess && result.Value != null) return Ok(result.Value);
            if (result.IsSuccess && result.Value == null) return NotFound();
            return BadRequest(result.ErrorMsg);
        }

        [HttpPost]
        public async Task<IActionResult> PostRelation([FromBody] Relation relation)
        {
            var result = await _mediator.Send(new Create.Command { Relation = relation });
            if (result == null) return NotFound();
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg);
        }

        [HttpDelete("{relationId}")]
        public async Task<IActionResult> DeleteRelation(Guid relationId)
        {
            var result = await _mediator.Send(new Delete.Command { Id = relationId });

            if (result == null) return NotFound();
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg);
        }

        [HttpPut("{relationId}")]
        public async Task<IActionResult> PutRelation(Guid relationId, Relation relation, Guid treeId)
        {
            var result = await _mediator.Send(new Edit.Command { Relation = relation, Id = relationId});

            if (result == null) return NotFound();
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg);
        }
    }
}
