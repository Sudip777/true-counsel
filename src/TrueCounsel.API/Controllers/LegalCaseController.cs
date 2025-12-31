using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrueCounsel.API.Models.Requests;
using TrueCounsel.Application.Features.LegalCase.Commands;
using TrueCounsel.Application.Features.LegalCase.Queries;

namespace TrueCounsel.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LegalCaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LegalCaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllLegalCasesQuery());
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetLegalCaseById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetLegalCaseByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLegalCaseRequest request)
        {
            var command = new CreateLegalCaseCommand
            {
                CaseNumber = request.CaseNumber,
                CourtCaseNumber = request.CourtCaseNumber,
                ClientId = request.ClientId,
                LawyerId = request.LawyerId,
                CaseCategoryId = request.CaseCategoryId,
                CourtId = request.CourtId,
                CaseTypeId = request.CaseTypeId,
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                Priority = request.Priority,
                FilingDate = request.FilingDate,
                ClosingDate = request.ClosingDate,
                Outcome = request.Outcome,
                OutcomeNotes = request.OutcomeNotes,
                Tags = request.Tags,
                EstimatedValue = request.EstimatedValue
            };

            var result = await _mediator.Send(command);
            return CreatedAtRoute("GetLegalCaseById", new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLegalCaseRequest request)
        {
            if (id != request.Id) return BadRequest("ID mismatch");

            var command = new UpdateLegalCaseCommand
            {
                Id = id,
                CaseNumber = request.CaseNumber,
                CourtCaseNumber = request.CourtCaseNumber,
                ClientId = request.ClientId,
                LawyerId = request.LawyerId,
                CaseCategoryId = request.CaseCategoryId,
                CourtId = request.CourtId,
                CaseTypeId = request.CaseTypeId,
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                Priority = request.Priority,
                FilingDate = request.FilingDate,
                ClosingDate = request.ClosingDate,
                Outcome = request.Outcome,
                OutcomeNotes = request.OutcomeNotes,
                Tags = request.Tags,
                EstimatedValue = request.EstimatedValue
            };

            var result = await _mediator.Send(command);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteLegalCaseCommand(id));
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
