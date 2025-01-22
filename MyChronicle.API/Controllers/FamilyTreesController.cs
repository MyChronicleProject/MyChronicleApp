using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyChronicle.Application;
using MyChronicle.Application.FamilyTrees;
using MyChronicle.Domain;
using System.Security.Claims;

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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (result.IsSuccess && result.Value != null)
            {
                var authorizedResults = result.Value.Where(ft => ft.FamilyTreePermisions.Any(ftp => ftp.AppUser.Id == userId));
                return Ok(authorizedResults);
            }

            if (!result.IsSuccess && result.ErrorMsg!.Category == ErrorCategory.NotFound) return NotFound(result.ErrorMsg!.Message);
            if (result.IsSuccess && result.Value == null) return NotFound();
            return BadRequest(result.ErrorMsg!.Message);
        }

        [HttpGet("{treeId}")]
        public async Task<IActionResult> GetFamilyTree(Guid treeId)
        {
            var result = await _mediator.Send(new Details.Query { Id = treeId });
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (result.IsSuccess && result.Value != null)
            {
                if (result.Value.FamilyTreePermisions.Any(ftp => ftp.AppUser.Id == userId))
                {
                    return Ok(result.Value);
                }
                return Forbid();
            }

            if (!result.IsSuccess && result.ErrorMsg!.Category == ErrorCategory.NotFound) return NotFound(result.ErrorMsg!.Message);
            if (result.IsSuccess && result.Value == null) return NotFound();
            return BadRequest(result.ErrorMsg!.Message);
        }

        [HttpPost]
        public async Task<IActionResult> PostFamilyTree(FamilyTreeDTO familyTreeDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(new Create.Command { FamilyTreeDTO = familyTreeDTO, OwnerId = userId });

            if (!result.IsSuccess && result.ErrorMsg!.Category == ErrorCategory.NotFound) return NotFound(result.ErrorMsg!.Message);
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg!.Message);
        }

        [HttpPut("{treeId}")]
        public async Task<IActionResult> PutFamilyTree(Guid treeId, FamilyTree familyTree)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var ft = await _mediator.Send(new Details.Query { Id = treeId });
            if (ft.Value != null && ft.Value.FamilyTreePermisions.Where(ftp => ftp.AppUser.Id == userId).First().Role != Role.Author)
            {
                return Forbid();
            }

            var result = await _mediator.Send(new Edit.Command { FamilyTree = familyTree, Id = treeId });

            if (!result.IsSuccess && result.ErrorMsg!.Category == ErrorCategory.NotFound) return NotFound(result.ErrorMsg!.Message);
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg!.Message);
        }

        [HttpDelete("{treeId}")]
        public async Task<IActionResult> DeleteFamilyTree(Guid treeId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var ft = await _mediator.Send(new Details.Query { Id = treeId });
            if (ft.Value != null && ft.Value.FamilyTreePermisions.Where(ftp => ftp.AppUser.Id == userId).First().Role != Role.Author)
            {
                return Forbid();
            }

            var result = await _mediator.Send(new Delete.Command { Id = treeId });

            if (!result.IsSuccess && result.ErrorMsg!.Category == ErrorCategory.NotFound) return NotFound(result.ErrorMsg!.Message);
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg!.Message);
        }
    }
}
