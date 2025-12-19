namespace TrueCounsel.Application.Features.Auth.Commands
{
    public record RegisterAuthCommand(
     string Name,
     string Email,
     string Password
 );
}
