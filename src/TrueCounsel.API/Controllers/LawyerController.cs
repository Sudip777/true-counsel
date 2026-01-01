using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Lawyer.Commands;
using TrueCounsel.Application.Features.Lawyer.Dtos;
using TrueCounsel.Application.Features.Lawyer.Queries;

namespace TrueCounsel.API.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    
    public class LawyerController : ControllerBase
    {
        private readonly ICommandHandler<RegisterLawyerCommand, LawyerDto> _registerHandler;
        private readonly ICommandHandler<UpdateLawyerCommand, LawyerDto> _updateHandler;
        private readonly ICommandHandler<DeleteLawyerCommand, bool> _deleteHandler;
        private readonly IQueryHandler<GetAllLawyersQuery, IEnumerable<LawyerDto>> _getAllHandler;
        private readonly IQueryHandler<GetLawyerByIdQuery, LawyerDto?> _getByIdHandler;

        public LawyerController(
            ICommandHandler<RegisterLawyerCommand, LawyerDto> registerHandler,
            ICommandHandler<UpdateLawyerCommand, LawyerDto> updateHandler,
            ICommandHandler<DeleteLawyerCommand, bool> deleteHandler,
            IQueryHandler<GetAllLawyersQuery, IEnumerable<LawyerDto>> getAllHandler,
            IQueryHandler<GetLawyerByIdQuery, LawyerDto?> getByIdHandler)
        {
            _registerHandler = registerHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _getAllHandler = getAllHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary> Gets all lawers </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LawyerDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllHandler.HandleAsync(new GetAllLawyersQuery());
            return Ok(result);
        }

        /// <summary> Gets a lawyer by id  </summary>
        [HttpGet("{id}", Name = "GetLawyerById")]
        [ProducesResponseType(typeof(LawyerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.HandleAsync(new GetLawyerByIdQuery(id));
            if(result == null) return NotFound();
            return Ok(result);
        }

        /// <summary> Registers new lawer </summary>
        [HttpPost]
        [ProducesResponseType(typeof(LawyerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterLawyerCommand command)
        {
            var result = await _registerHandler.HandleAsync(command);
            return CreatedAtRoute("GetLawyerById", new { id = result.Id }, result);
        }

        /// <summary> Updates an existing lawyer </summary>
        [HttpPut]
        [ProducesResponseType(typeof(LawyerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateLawyerCommand command)
        {
            var result = await _updateHandler.HandleAsync(command);
            return Ok(result);
        }

        /// <summary> Deletes a lawyer </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _deleteHandler.HandleAsync(new DeleteLawyerCommand { Id = id });
            if(!result) return NotFound();
            return NoContent();
        }
    }
}
