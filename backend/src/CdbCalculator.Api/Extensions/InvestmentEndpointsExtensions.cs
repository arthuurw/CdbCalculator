using CdbCalculator.Api.Endpoints.Cdb;

namespace CdbCalculator.Api.Extensions;

/// <summary>
/// Provides extension methods to map investment-related endpoints.
/// </summary>
public static class InvestmentEndpointsExtensions
{
    /// <summary>
    /// Maps all investment endpoints.
    /// </summary>
    /// <param name="app">The endpoint route builder.</param>
    /// <returns>The endpoint route builder.</returns>
    public static IEndpointRouteBuilder MapInvestmentEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCalculateCdbEndpoint();
        return app;
    }
}