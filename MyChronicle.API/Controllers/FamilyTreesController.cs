﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
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
            return BadRequest(result.ErrorMsg);
        }

        [HttpGet("{treeId}")]
        public async Task<IActionResult> GetFamilyTree(Guid treeId)
        {
            var result = await _mediator.Send(new Details.Query { Id = treeId });

            if (result == null) return NotFound();
            if (result.IsSuccess && result.Value != null) return Ok(result.Value);
            if (result.IsSuccess && result.Value == null) return NotFound();
            return BadRequest(result.ErrorMsg);
        }

        [HttpPost]
        public async Task<IActionResult> PostFamilyTree(FamilyTree familyTree)
        {
            var result = await _mediator.Send(new Create.Command { FamilyTree = familyTree });
            
            if (result == null) return NotFound();
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg);
        }

        [HttpPut("{treeId}")]
        public async Task<IActionResult> PutFamilyTree(Guid treeId, FamilyTree familyTree)
        {
            var result = await _mediator.Send(new Edit.Command { FamilyTree = familyTree, Id = treeId });

            if (result == null) return NotFound();
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg);
        }

        [HttpDelete("{treeId}")]
        public async Task<IActionResult> DeleteFamilyTree(Guid treeId)
        {
            var result = await _mediator.Send(new Delete.Command { Id = treeId });

            if (result == null) return NotFound();
            if (result.IsSuccess) return Ok(result.Value);
            return BadRequest(result.ErrorMsg);
        }
    }
}
