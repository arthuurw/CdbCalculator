using CdbCalculator.Application.Investments.CalculateCdb;
using CdbCalculator.Application.Investments.CalculateCdb.Interfaces;

namespace CdbCalculator.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICalculateCdbService, CalculateCdbService>();
        return services;
    }
}