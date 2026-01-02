using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Models;
using TrueCounsel.Application.Features.Auth.Commands;
using TrueCounsel.Application.Features.Auth.Dtos;

namespace TrueCounsel.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary> Authenticate user </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);
            var command = new LoginCommand(request.Email, request.Password);
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        /// <summary> Registers a new user. </summary>
        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterAuthRequestDto request)
        {
            ArgumentNullException.ThrowIfNull(request);
            var command = new RegisterAuthCommand(
                 request.Name,
                 request.Email,
                 request.Password
            );

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
