using CdbCalculator.Api.Endpoints.Cdb;

namespace CdbCalculator.Api.Extensions;

public static class InvestmentEndpointsExtensions
{
    public static IEndpointRouteBuilder MapInvestmentEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCalculateCdbEndpoint();
        return app;
    }
}