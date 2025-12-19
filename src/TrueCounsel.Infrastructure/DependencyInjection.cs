using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Infrastructure.Persistence.Security;

namespace TrueCounsel.Infrastructure
{
    public static class DependencyInjectionItems  // ← Added 'static class'
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Register HttpContextAccessor
            services.AddHttpContextAccessor();

            // Register services with proper type arguments
            services.AddScoped<IClaimsInterface, ClaimInterface>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}