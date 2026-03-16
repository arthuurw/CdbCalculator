using CdbCalculator.Application.Investments.CalculateCdb;
using CdbCalculator.Application.Investments.CalculateCdb.Interfaces;

namespace CdbCalculator.Api.Extensions;

/// <summary>
/// Provides extension methods to register application services in the dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers application layer services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<ICalculateCdbService, CalculateCdbService>();

        return services;
    }
}