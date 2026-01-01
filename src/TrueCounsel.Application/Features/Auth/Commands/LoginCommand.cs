using MediatR;
using TrueCounsel.Application.Features.Auth.Dtos;

namespace TrueCounsel.Application.Features.Auth.Commands
{
    public record LoginCommand(string Email, string Password) : IRequest<LoginResponseDto>;
}
