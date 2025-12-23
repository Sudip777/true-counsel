using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueCounsel.API.Models.Requests;
using TrueCounsel.Application.Features.Client.Commands;
using TrueCounsel.Application.Features.Client.Queries;

namespace TrueCounsel.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllClientsQuery());
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetClientByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientRequest request)
        {
            var command = new CreateClientCommand
            {
                UserId = request.UserId,
                Phone = request.Phone,
                Address = request.Address,
                DateOfBirth = request.DateOfBirth,
                Occupation = request.Occupation,
                EmergencyContact = request.EmergencyContact
            };

            var result = await _mediator.Send(command);
            return CreatedAtRoute(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateClientRequest request)
        {
            var command = new UpdateClientCommand
            {
                Id = id,
                UserId = request.UserId,
                Phone = request.Phone,
                Address = request.Address,
                DateOfBirth = request.DateOfBirth,
                Occupation = request.Occupation,
                EmergencyContact = request.EmergencyContact
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteClientCommand(id));
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
