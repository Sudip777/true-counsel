using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueCounsel.Application.Features.CaseCategory.Commands;
using TrueCounsel.Application.Features.CaseCategory.Dtos;
using TrueCounsel.Application.Features.CaseCategory.Queries;

namespace TrueCounsel.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CaseCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CaseCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCaseCategoryQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCaseCategoryCommand request)
        {
            if (request == null)
                return BadRequest("Request cannot be null");

            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetAll), result);
        }
    }
}
