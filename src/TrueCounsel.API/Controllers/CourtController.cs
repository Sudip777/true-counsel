using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueCounsel.Application.Features.Court.Commands;
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

        /// <summary>
        /// Retrieves all active courts (excluding soft-deleted records).
        /// </summary>
        /// <returns>List of court DTOs</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCourtsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific court by ID.
        /// Returns 404 if the court is not found or has been soft-deleted.
        /// </summary>
        /// <param name="id">The court ID</param>
        /// <returns>Court DTO if found</returns>
        [HttpGet("{id}", Name = "GetCourtById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCourtByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Creates a new court.
        /// </summary>
        /// <param name="request">Court creation request containing name, address, city, state, and court type ID</param>
        /// <returns>Created court DTO with assigned ID</returns>
        [HttpPost]
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
            return CreatedAtRoute(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates an existing court.
        /// </summary>
        /// <param name="id">The court ID to update</param>
        /// <param name="request">Court update request containing updated fields</param>
        /// <returns>Updated court DTO</returns>
        [HttpPut("{id}")]
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

        /// <summary>
        /// Soft deletes a court by ID.
        /// The record is preserved in the database with a DeletedAt timestamp for audit purposes.
        /// Subsequent queries (GetAll, GetById) will exclude the soft-deleted record.
        /// </summary>
        /// <param name="id">The court ID to delete</param>
        /// <returns>NoContent (204) on success, NotFound (404) if court not found</returns>
        [HttpDelete("{id}")]
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
