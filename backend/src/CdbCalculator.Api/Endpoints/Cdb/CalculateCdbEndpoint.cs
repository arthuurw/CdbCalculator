using CdbCalculator.Application.Investments.CalculateCdb;
using CdbCalculator.Application.Investments.CalculateCdb.Interfaces;

namespace CdbCalculator.Api.Endpoints.Cdb;

public static class CalculateCdbEndpoint
{
    public static RouteHandlerBuilder MapCalculateCdbEndpoint(this IEndpointRouteBuilder app)
    {
        return app.MapPost("/api/investments/cdb/calculate",
            static (CalculateCdbRequest request, ICalculateCdbService service) =>
            {
                CalculateCdbResponse response = service.Calculate(request);
                return Results.Ok(response);
            })
            .WithName("CalculateCdb")
            .WithSummary("Calculates gross and net CDB investment values.")
            .WithDescription("Calculates the gross and net amount of a CDB investment based on the initial amount and redemption term in months.")
            .Produces<CalculateCdbResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}