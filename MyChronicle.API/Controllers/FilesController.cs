using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyChronicle.Application.Files;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;

namespace MyChronicle.API.Controllers
{
    [Route("api/FamilyTrees/{treeId}/Person/{personId}/[controller]")]
    public class FilesController : BaseAPIController
    {
        private readonly IMediator _mediator;
        public FilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetFiles(Guid treeId, Guid personId)
        {

            var result = await _mediator.Send(new List.Query { PersonId = personId });

            if (result == null) return NotFound();
            if (result.IsSuccess && result.Value != null) return Ok(result.Value);
            if (result.IsSuccess && result.Value == null) return NotFound();
            return BadRequest(result.ErrorMsg);
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> GetFile(Guid treeId, Guid personId, Guid fileId)
        {

            var result = await _mediator.Send(new Details.Query { Id = fileId });

            if (result == null) return NotFound();
            if (result.IsSuccess && result.Value != null) return Ok(result.Value);
            if (result.IsSuccess && result.Value == null) return NotFound();
            return BadRequest(result.ErrorMsg);
        }

        [HttpPost]
        public async Task<IActionResult> PostFile([FromBody] MyChronicle.Domain.File file)
        {
            var result = await _mediator.Send(new Create.Command { File = file });
            if (result == null) return NotFound();
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg);
        }

        [HttpDelete("{fileId}")]
        public async Task<IActionResult> DeleteFile(Guid fileId)
        {
            var result = await _mediator.Send(new Delete.Command { Id = fileId });

            if (result == null) return NotFound();
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg);
        }

        [HttpPut("{fileId}")]
        public async Task<IActionResult> PutFile(Guid fileId, Domain.File file, Guid treeId)
        {
            var result = await _mediator.Send(new Edit.Command { File = file, Id = fileId });

            if (result == null) return NotFound();
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg);
        }
    }


}
