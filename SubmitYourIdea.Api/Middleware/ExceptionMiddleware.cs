using System.Text.Json;
using SubmitYourIdea.ApiModels.Api;

namespace SubmitYourIdea.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = e.Message
            };
            var response = new ApiResponse<object>
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                ProblemDetails = problemDetails
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}