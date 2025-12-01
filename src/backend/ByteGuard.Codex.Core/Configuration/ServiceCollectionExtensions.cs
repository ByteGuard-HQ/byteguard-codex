using Microsoft.Extensions.DependencyInjection;

namespace ByteGuard.Codex.Core.Configuration;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add core dependencies to the given service collection.
    /// </summary>
    /// <param name="services">Sevice collection.</param>
    public static IServiceCollection AddCodexCore(this IServiceCollection services)
    {
        return services;
    }
}
