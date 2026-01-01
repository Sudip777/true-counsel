using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrueCounsel.API.Models.Requests;
using TrueCounsel.Application.Features.CaseParty.Commands;
using TrueCounsel.Application.Features.CaseParty.Dtos;
using TrueCounsel.Application.Features.CaseParty.Queries;

namespace TrueCounsel.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CasePartyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CasePartyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CasePartyDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCasePartiesQuery());
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetCasePartyById")]
        [ProducesResponseType(typeof(CasePartyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCasePartyByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("case/{caseId}")]
        [ProducesResponseType(typeof(IEnumerable<CasePartyDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCaseId(int caseId)
        {
            var result = await _mediator.Send(new GetCasePartiesByCaseIdQuery(caseId));
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CasePartyDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCasePartyRequest request)
        {
            var command = new CreateCasePartyCommand
            {
                CaseId = request.CaseId,
                Name = request.Name,
                Role = request.Role,
                Organization = request.Organization,
                ContactEmail = request.ContactEmail,
                ContactPhone = request.ContactPhone,
                Address = request.Address,
                Notes = request.Notes
            };

            var result = await _mediator.Send(command);
            return CreatedAtRoute("GetCasePartyById", new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CasePartyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCasePartyRequest request)
        {
            var command = new UpdateCasePartyCommand
            {
                Id = id,
                Name = request.Name,
                Role = request.Role,
                Organization = request.Organization,
                ContactEmail = request.ContactEmail,
                ContactPhone = request.ContactPhone,
                Address = request.Address,
                Notes = request.Notes
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCasePartyCommand(id));
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
