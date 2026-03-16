using CdbCalculator.Api.Middleware;

namespace CdbCalculator.Api.Extensions;

/// <summary>
/// Provides extension methods to configure middleware in the HTTP request pipeline.
/// </summary>
public static class MiddlewareExtensions
{
    /// <summary>
    /// Adds the global exception handling middleware to the application pipeline.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The application builder.</returns>
    public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}