﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyChronicle.Application;
using MyChronicle.Application.Persons;
using MyChronicle.Domain;

namespace MyChronicle.API.Controllers
{
    [Route("api/FamilyTrees/{treeId}/[controller]")]
    public class PersonsController : BaseAPIController
    {
        private readonly IMediator _mediator;
        public PersonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPersons(Guid treeId)
        {
            var result = await _mediator.Send(new List.Query { FamilyTreeId = treeId });

            if (!result.IsSuccess && result.ErrorMsg!.Category == ErrorCategory.NotFound) return NotFound(result.ErrorMsg!.Message);
            if (result.IsSuccess && result.Value != null) return Ok(result.Value);
            if (result.IsSuccess && result.Value == null) return NotFound();
            return BadRequest(result.ErrorMsg!.Message);
        }

        [HttpGet("{personId}")]
        public async Task<IActionResult> GetPerson(Guid treeId, Guid personId)
        {
            var result = await _mediator.Send(new Details.Query { Id = personId, FamilyTreeId = treeId });

            if (!result.IsSuccess && result.ErrorMsg!.Category == ErrorCategory.NotFound) return NotFound(result.ErrorMsg!.Message);
            if (result.IsSuccess && result.Value != null) return Ok(result.Value);
            if (result.IsSuccess && result.Value == null) return NotFound();
            return BadRequest(result.ErrorMsg!.Message);
        }

        [HttpPost]
        public async Task<IActionResult> PostPerson(PersonDTO personDTO, Guid treeId)
        {
            var result = await _mediator.Send(new Create.Command { PersonDTO = personDTO, FamilyTreeId = treeId });

            if (!result.IsSuccess && result.ErrorMsg!.Category == ErrorCategory.NotFound) return NotFound(result.ErrorMsg!.Message);
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg!.Message);
        }

        [HttpPut("{personId}")]
        public async Task<IActionResult> PutPerson(Guid personId, PersonDTO personDTO, Guid treeId)
        {
            var result = await _mediator.Send(new Edit.Command { PersonDTO = personDTO, Id = personId, FamilyTreeId = treeId });

            if (!result.IsSuccess && result.ErrorMsg!.Category == ErrorCategory.NotFound) return NotFound(result.ErrorMsg!.Message);
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg!.Message);
        }

        [HttpDelete("{personId}")]
        public async Task<IActionResult> DeletePerson(Guid personId, Guid treeId)
        {
            var result = await _mediator.Send(new Delete.Command { Id = personId, FamilyTreeId = treeId });

            if (!result.IsSuccess && result.ErrorMsg!.Category == ErrorCategory.NotFound) return NotFound(result.ErrorMsg!.Message);
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg!.Message);
        }
    }
}
