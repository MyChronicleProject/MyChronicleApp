using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyChronicle.Application;
using MyChronicle.Domain;
using System.Security.Claims;
using MyChronicle.Application.FamilyTreePermisions;

namespace MyChronicle.API.Controllers
{
    public class FamilyTreePermisionController:BaseAPIController
    {
        private readonly IMediator _mediator;
        public FamilyTreePermisionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{treeId}")]
        public async Task<IActionResult> DeleteFamilyTree(Guid treeId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
          
            var result = await _mediator.Send(new Delete.Command { Id = treeId,AppUser = userId });

            if (!result.IsSuccess && result.ErrorMsg!.Category == ErrorCategory.NotFound) return NotFound(result.ErrorMsg!.Message);
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg!.Message);
        }

    }
}
