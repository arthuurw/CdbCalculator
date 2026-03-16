using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace CdbCalculator.Api.Middleware;

/// <summary>
/// Middleware responsible for handling unhandled exceptions globally
/// and converting them into standardized HTTP responses.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
/// </remarks>
/// <param name="next">The next middleware in the pipeline.</param>
/// <param name="logger">The logger instance.</param>
public sealed class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    /// <summary>
    /// Processes the HTTP request and handles unhandled exceptions.
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ArgumentException ex)
        {
            await WriteProblemDetailsAsync(
                context,
                HttpStatusCode.BadRequest,
                "Invalid request",
                ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");

            await WriteProblemDetailsAsync(
                context,
                HttpStatusCode.InternalServerError,
                "Internal Server Error",
                "An unexpected error occurred.");
        }
    }

    private static async Task WriteProblemDetailsAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string title,
        string detail)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)statusCode;

        var problem = new ProblemDetails
        {
            Title = title,
            Detail = detail,
            Status = (int)statusCode,
            Instance = context.Request.Path
        };

        await context.Response.WriteAsJsonAsync(problem);
    }
}