using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Common.Models;
using TrueCounsel.Application.Features.Auth.Commands;
using TrueCounsel.Application.Features.Auth.Dtos;

namespace TrueCounsel.API.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ICommandHandler<LoginCommand, LoginResponseDto> _loginHandler;
        private readonly ICommandHandler<RegisterAuthCommand, UserDto> _registerHandler;

        public AuthController(
             ICommandHandler<LoginCommand, LoginResponseDto> loginHandler,
             ICommandHandler<RegisterAuthCommand, UserDto> registerHandler)
        {
            _loginHandler = loginHandler;
            _registerHandler = registerHandler;
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
        var result = await _loginHandler.HandleAsync(command);

            return Ok(result);
        }


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

            var result = await _registerHandler.HandleAsync(command);
            return Ok(result);

        }
    }
}
