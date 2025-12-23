using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace TrueCounsel.API.Middlewares
{
    internal sealed class ProblemDetailsExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProblemDetailsExceptionMiddleware> _logger;

        public ProblemDetailsExceptionMiddleware(RequestDelegate next, ILogger<ProblemDetailsExceptionMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex).ConfigureAwait(false);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var problem = new ProblemDetails
            {
                Type = "https://example.com/probs/internal",
                Title = "An error occurred while processing your request.",
                Status = StatusCodes.Status500InternalServerError,
                Detail = exception.Message,
                Instance = context.Request.Path
            };
            if (exception is AppValidationException validationException)
            {
                problem.Status = StatusCodes.Status400BadRequest;
                problem.Title = "Validation Error";
                problem.Detail = "One or more validation errors occurred.";
                problem.Extensions.Add("errors", validationException.Errors);
            }
           

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problem.Status ?? 500;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            return context.Response.WriteAsync(JsonSerializer.Serialize(problem, options));
        }
    }
}