using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueCounsel.Application.Features.Lawyer.Commands;
using TrueCounsel.Application.Features.Lawyer.Dtos;
using TrueCounsel.Application.Features.Lawyer.Queries;

namespace TrueCounsel.API.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LawyerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LawyerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary> Gets all lawyers. </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LawyerDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllLawyersQuery());
            return Ok(result);
        }

        /// <summary> Gets a lawyer by id. </summary>
        [HttpGet("{id}", Name = "GetLawyerById")]
        [ProducesResponseType(typeof(LawyerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetLawyerByIdQuery(id));
            if(result == null) return NotFound();
            return Ok(result);
        }

        /// <summary> Registers a new lawyer. </summary>
        [HttpPost]
        [ProducesResponseType(typeof(LawyerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterLawyerCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtRoute("GetLawyerById", new { id = result.Id }, result);
        }

        /// <summary> Updates an existing lawyer. </summary>
        [HttpPut]
        [ProducesResponseType(typeof(LawyerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateLawyerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary> Deletes a lawyer. </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteLawyerCommand { Id = id });
            if(!result) return NotFound();
            return NoContent();
        }
    }
}
