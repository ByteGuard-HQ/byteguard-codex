using ByteGuard.Codex.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ByteGuard.Codex.Core.Configuration;

/// <summary>
/// Codex core service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add core dependencies to the given service collection.
    /// </summary>
    /// <param name="services">Sevice collection.</param>
    public static IServiceCollection AddCodexCore(this IServiceCollection services)
    {
        // Add services.
        services.AddTransient<AsvsService>();
        services.AddTransient<ProjectService>();

        return services;
    }
}
