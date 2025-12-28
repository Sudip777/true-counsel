using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueCounsel.Application.Features.CaseType.Commands;
using TrueCounsel.Application.Features.CaseType.Dtos;
using TrueCounsel.Application.Features.CaseType.Queries;

namespace TrueCounsel.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CaseTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CaseTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all case types
        /// </summary>
        /// <returns>A list of all case types</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCaseTypeQuery());
            return Ok(result);
        }

        /// <summary>
        /// Create a new case type
        /// </summary>
        /// <param name="request">Case type details</param>
        /// <returns>The created case type</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCaseTypeCommand request)
        {
            if (request == null)
                return BadRequest("Request cannot be null");

            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetAll), result);
        }
    }
}
