using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.API.Middlewares;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Auth.Commands;
using TrueCounsel.Application.Features.Auth.Commands.Handlers;
using TrueCounsel.Application.Features.Auth.Dtos;
using TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;
using TrueCounsel.Infrastructure;
using TrueCounsel.Infrastructure.Data;
using TrueCounsel.Infrastructure.Data.Repositories;
using TrueCounsel.Infrastructure.Persistence.Security;

var builder = WebApplication.CreateBuilder(args);

// Essential: Add built-in OpenAPI document generation
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        // Add API Info
        document.Info = new OpenApiInfo
        {
            Title = "True Counsel Law System API",
            Version = "v1.0",
            Description = "API for managing legal cases, clients, and authentication"
        };

        document.Components ??= new OpenApiComponents();

        document.Components.SecuritySchemes["BearerAuth"] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Description = "Enter a valid JWT token"
        };

        // Apply the security requirement globally
        document.SecurityRequirements.Add(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "BearerAuth" }
                },
                new string[] { }
            }
        });

        return Task.CompletedTask;
    });
});

builder.Services.AddHttpContextAccessor();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
// Register your services
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
//builder.Services.AddScoped<ICommandHandler<RegisterAuthCommand, User>, RegisterAuthCommandHandler>();
builder.Services.AddScoped<ICommandHandler<LoginCommand, LoginResponseDto>, LoginCommandHandler>();
builder.Services.AddInfrastructure();
builder.Services.AddScoped<IUserRepository, UserRepository>();


// JWT Authentication
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
    };
});
builder.Services.AddAuthorization();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers(); 

// Configuration loading (move this earlier if needed, but it's fine here too)
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                     .AddEnvironmentVariables();

var app = builder.Build();

// Expose the OpenAPI JSON document (required for Scalar to fetch it)
app.MapOpenApi();

// Global exception middleware
app.UseMiddleware<ProblemDetailsExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();  

// Scalar only in development
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("True Counsel Api")
               .WithTheme(ScalarTheme.BluePlanet)
               .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
               .AddPreferredSecuritySchemes("BearerAuth")
               .AddHttpAuthentication("BearerAuth", auth =>
               {
                   auth.Token = "";  // Placeholder – user will fill in real token
               });
    });
}

app.Run();