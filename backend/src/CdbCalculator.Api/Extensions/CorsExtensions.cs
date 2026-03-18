namespace CdbCalculator.Api.Extensions;

/// <summary>
/// Provides extension methods to configure CORS policies.
/// </summary>
public static class CorsExtensions
{
    private const string DefaultPolicy = "DefaultCorsPolicy";

    /// <summary>
    /// Adds CORS configuration to the service collection.
    /// </summary>
    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        string[] allowedOrigins = configuration
            .GetSection("Cors:AllowedOrigins")
            .Get<string[]>() ?? [];

        services.AddCors(options =>
        {
            options.AddPolicy(DefaultPolicy, policy =>
            {
                policy
                    .WithOrigins(allowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        return services;
    }

    /// <summary>
    /// Applies the configured CORS policy to the application.
    /// </summary>
    public static IApplicationBuilder UseCorsConfiguration(this IApplicationBuilder app)
    {
        app.UseCors(DefaultPolicy);
        return app;
    }
}