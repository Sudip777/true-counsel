using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueCounsel.Application.Features.CaseNote.Commands;
using TrueCounsel.Application.Features.CaseNote.Dtos;
using TrueCounsel.Application.Features.CaseNote.Queries;

namespace TrueCounsel.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CaseNoteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CaseNoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary> Gets all case notes. </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CaseNoteDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCaseNoteQuery());
            return Ok(result);
        }

        /// <summary> Gets a case note by id. </summary>
        [HttpGet("{id}", Name = "GetCaseNoteById")]
        [ProducesResponseType(typeof(CaseNoteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCaseNoteByIdQuery(id));
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary> Creates a new case note. </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CaseNoteDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCaseNoteCommand request)
        {
            if (request == null)
                return BadRequest("Request cannot be null");

            var result = await _mediator.Send(request);
            return CreatedAtRoute("GetCaseNoteById", new { id = result.Id }, result);
        }

        /// <summary> Deletes a case note. </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCaseNoteCommand(id));
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
