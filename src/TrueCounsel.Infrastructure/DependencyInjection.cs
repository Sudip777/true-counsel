using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Application.Features.Auth.Commands;
using TrueCounsel.Application.Features.Auth.Commands.Handlers;
using TrueCounsel.Application.Features.Auth.Dtos;
using TrueCounsel.Application.Features.Lawyer.Commands;
using TrueCounsel.Application.Features.Lawyer.Commands.Handlers;
using TrueCounsel.Application.Features.Lawyer.Dtos;
using TrueCounsel.Application.Features.Lawyer.Queries;
using TrueCounsel.Application.Features.Lawyer.Queries.Handlers;
using TrueCounsel.Application.Features.Client.Commands;
using TrueCounsel.Application.Features.Client.Commands.Handlers;
using TrueCounsel.Application.Features.Client.Dtos;
using TrueCounsel.Application.Features.Client.Queries;
using TrueCounsel.Application.Features.Client.Queries.Handlers;
using TrueCounsel.Application.Common.Models;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Domain.Interfaces;
using TrueCounsel.Infrastructure.Data;
using TrueCounsel.Infrastructure.Data.Repositories;
using TrueCounsel.Infrastructure.Persistence.Repositories;
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

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILawyerRepository, LawyerRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Handlers
            services.AddScoped<ICommandHandler<LoginCommand, LoginResponseDto>, LoginCommandHandler>();
            services.AddScoped<ICommandHandler<RegisterAuthCommand, UserDto>, RegisterAuthCommandHandler>();
            
            services.AddScoped<ICommandHandler<RegisterLawyerCommand, LawyerDto>, RegisterLawyerHandler>();
            services.AddScoped<ICommandHandler<UpdateLawyerCommand, LawyerDto>, UpdateLawyerHandler>();
            services.AddScoped<ICommandHandler<DeleteLawyerCommand, bool>, DeleteLawyerHandler>();

            services.AddScoped<IQueryHandler<GetAllLawyersQuery, IEnumerable<LawyerDto>>, GetAllLawyersHandler>();
            services.AddScoped<IQueryHandler<GetLawyerByIdQuery, LawyerDto?>, GetLawyerByIdHandler>();

            return services;
        }
    }
}