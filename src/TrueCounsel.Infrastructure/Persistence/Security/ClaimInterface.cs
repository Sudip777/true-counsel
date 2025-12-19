using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TrueCounsel.Application.Common.Abstractions;

namespace TrueCounsel.Infrastructure.Persistence.Security
{
        public class ClaimInterface : IClaimsInterface
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            public ClaimInterface(IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
            }
        public string? Username => _httpContextAccessor.HttpContext!.User?.FindFirst(ClaimTypes.Name)?.Value;

        public string? Role => _httpContextAccessor.HttpContext!.User?.FindFirst(ClaimTypes.Role)?.Value;

        public string? UserId => _httpContextAccessor.HttpContext!.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    }

}
