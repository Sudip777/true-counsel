using MediatR;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCaseNoteQuery());
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetCaseNoteById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCaseNoteByIdQuery(id));
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCaseNoteCommand request)
        {
            if (request == null)
                return BadRequest("Request cannot be null");

            var result = await _mediator.Send(request);
            return CreatedAtRoute("GetCaseNoteById", new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCaseNoteCommand(id));
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
