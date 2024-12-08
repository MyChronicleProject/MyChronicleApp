using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyChronicle.Application;
using MyChronicle.Application.Relations;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;
using System.Diagnostics.Eventing.Reader;

namespace MyChronicle.API.Controllers
{
   
    [Route("api/FamilyTrees/{treeId}/Persons/{personId}/[controller]")]
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

            if (!result.IsSuccess && result.ErrorMsg!.Category == ErrorCategory.NotFound) return NotFound(result.ErrorMsg!.Message);
            if (result.IsSuccess && result.Value != null) return Ok(result.Value);
            if (result.IsSuccess && result.Value == null) return NotFound();
            return BadRequest(result.ErrorMsg!.Message);
        }

        [HttpGet("{relationId}")]
        public async Task<IActionResult> GetRelation(Guid personId, Guid relationId)
        {

            var result = await _mediator.Send(new Details.Query { Id = relationId, PersonId = personId });

            if (!result.IsSuccess && result.ErrorMsg!.Category == ErrorCategory.NotFound) return NotFound(result.ErrorMsg!.Message);
            if (result.IsSuccess && result.Value != null) return Ok(result.Value);
            if (result.IsSuccess && result.Value == null) return NotFound();
            return BadRequest(result.ErrorMsg!.Message);
        }

        [HttpPost]
        public async Task<IActionResult> PostRelation(RelationDTO relationDTO, Guid personId)
        {
            var result = await _mediator.Send(new Create.Command { RelationDTO = relationDTO, PersonId = personId });

            if (!result.IsSuccess && result.ErrorMsg!.Category == ErrorCategory.NotFound) return NotFound(result.ErrorMsg!.Message);
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg!.Message);
        }

        [HttpDelete("{relationId}")]
        public async Task<IActionResult> DeleteRelation(Guid relationId, Guid personId)
        {
            var result = await _mediator.Send(new Delete.Command { Id = relationId, PersonId = personId });

            if (!result.IsSuccess && result.ErrorMsg!.Category == ErrorCategory.NotFound) return NotFound(result.ErrorMsg!.Message);
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg!.Message);
        }

        [HttpPut("{relationId}")]
        public async Task<IActionResult> PutRelation(Guid relationId, RelationDTO relationDTO, Guid personId)
        {
            var result = await _mediator.Send(new Edit.Command { RelationDTO = relationDTO, Id = relationId, PersonId = personId });

            if (!result.IsSuccess && result.ErrorMsg!.Category == ErrorCategory.NotFound) return NotFound(result.ErrorMsg!.Message);
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg!.Message);
        }
    }

    [Route("api/FamilyTrees/{treeId}/[controller]")]
    public class RelationsControllerForOneTree : BaseAPIController
    {
        private readonly IMediator _mediator;

        public RelationsControllerForOneTree(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetRelaitionsForOneTree(Guid treeId)
        {
            var result = await _mediator.Send(new ListForOneTree.Query { FamilyTreeId = treeId });

            if (!result.IsSuccess && result.ErrorMsg!.Category == ErrorCategory.NotFound)
                return NotFound(result.ErrorMsg!.Message);
            if (result.IsSuccess && result.Value != null) return Ok(result.Value);
            if (result.IsSuccess && result.Value == null) return NotFound();
            return BadRequest(result.ErrorMsg!.Message);
        }
    }
}
