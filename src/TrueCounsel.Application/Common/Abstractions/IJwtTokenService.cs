using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Application.Common.Abstractions
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        bool ValidateToken(string token);
        System.Security.Claims.ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
       
    }
}
