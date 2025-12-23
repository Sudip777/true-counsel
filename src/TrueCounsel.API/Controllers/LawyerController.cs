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
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllHandler.HandleAsync(new GetAllLawyersQuery());
            return Ok(result);
        }

        /// <summary> Gets a lawyer by id  </summary>
        [HttpGet("{id}", Name = "GetLawyerById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.HandleAsync(new GetLawyerByIdQuery(id));
            if(result == null) return NotFound();
            return Ok(result);
        }

        /// <summary> Registers new lawer </summary>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterLawyerCommand command)
        {
            var result = await _registerHandler.HandleAsync(command);
            return CreatedAtRoute("GetLawyerById", new { id = result.Id }, result);
        }

        /// <summary> Updates an existing lawyer </summary>
        [HttpPut]
        public async Task<IActionResult> Update(UpdateLawyerCommand command)
        {
            var result = await _updateHandler.HandleAsync(command);
            return Ok(result);
        }

        /// <summary> Deletes a lawyer </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _deleteHandler.HandleAsync(new DeleteLawyerCommand { Id = id });
            if(!result) return NotFound();
            return NoContent();
        }
    }
}
