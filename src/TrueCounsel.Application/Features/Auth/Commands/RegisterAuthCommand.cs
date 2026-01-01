using MediatR;
using TrueCounsel.Application.Common.Models;

namespace TrueCounsel.Application.Features.Auth.Commands
{
    public record RegisterAuthCommand(
     string Name,
     string Email,
     string Password
 ) : IRequest<UserDto>;
}
