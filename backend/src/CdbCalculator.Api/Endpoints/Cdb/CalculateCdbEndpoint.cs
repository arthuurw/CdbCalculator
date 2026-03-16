using CdbCalculator.Application.Investments.CalculateCdb;
using CdbCalculator.Application.Investments.CalculateCdb.Interfaces;

namespace CdbCalculator.Api.Endpoints.Cdb;

/// <summary>
/// Maps the endpoint responsible for creating CDB investment simulations.
/// </summary>
public static class CalculateCdbEndpoint
{
    /// <summary>
    /// Maps the endpoint that creates a CDB investment simulation and returns the gross and net results.
    /// </summary>
    /// <param name="app">The endpoint route builder.</param>
    /// <returns>The configured route handler builder.</returns>
    public static RouteHandlerBuilder MapCalculateCdbEndpoint(this IEndpointRouteBuilder app)
    {
        return app.MapPost("/api/investments/cdb/simulations",
            static (CalculateCdbRequest request, ICalculateCdbService service) =>
            {
                CalculateCdbResponse response = service.Calculate(request);
                return Results.Ok(response);
            })
            .WithName("CreateCdbSimulation")
            .WithTags("CDB")
            .WithSummary("Simulates a CDB investment and returns the gross and net results.")
            .WithDescription(
                "Creates a simulation of a CDB investment using the provided initial amount and term in months. " +
                "The simulation calculates the gross return using fixed CDI and TB rates and applies income tax " +
                "according to the redemption period.")
            .Produces<CalculateCdbResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithOpenApi();
    }
}