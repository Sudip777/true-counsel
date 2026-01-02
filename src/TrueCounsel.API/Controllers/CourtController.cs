using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueCounsel.Application.Features.Court.Commands;
using TrueCounsel.Application.Features.Court.Dtos;
using TrueCounsel.Application.Features.Court.Queries;

namespace TrueCounsel.API.Controllers
{
    /// <summary>
    /// API Controller for managing Court resources.
    /// Provides RESTful endpoints for CRUD operations on courts.
    /// Follows clean architecture with MediatR pattern for command/query handling.
    /// All delete operations perform soft deletes (preserving records for audit purposes).
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CourtController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourtController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary> Gets all courts. </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CourtDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCourtsQuery());
            return Ok(result);
        }

        /// <summary> Gets a specific court by id. </summary>
        [HttpGet("{id}", Name = "GetCourtById")]
        [ProducesResponseType(typeof(CourtDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCourtByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary> Creates a new court. </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CourtDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCourtRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateCourtCommand
            {
                Name = request.Name,
                Address = request.Address,
                City = request.City,
                State = request.State,
                CourtTypeId = request.CourtTypeId
            };

            var result = await _mediator.Send(command);
            return CreatedAtRoute("GetCourtById", new { id = result.Id }, result);
        }

        /// <summary> Updates an existing court. </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CourtDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCourtRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new UpdateCourtCommand
            {
                Id = id,
                Name = request.Name,
                Address = request.Address,
                City = request.City,
                State = request.State
            };

            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary> Soft deletes a court by id. </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCourtCommand(id));
            if (!result) return NotFound();
            return NoContent();
        }
    }

    /// <summary>
    /// Request model for creating a new court.
    /// </summary>
    public class CreateCourtRequest
    {
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int CourtTypeId { get; set; }
    }

    /// <summary>
    /// Request model for updating an existing court.
    /// </summary>
    public class UpdateCourtRequest
    {
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }
}
