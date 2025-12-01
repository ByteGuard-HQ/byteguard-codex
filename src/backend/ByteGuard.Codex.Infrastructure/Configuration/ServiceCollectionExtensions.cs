using Microsoft.Extensions.DependencyInjection;

namespace ByteGuard.Codex.Infrastructure.Configuration;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Sqlite dependencies to the service collection.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="connectionString">Connection string.</param>
    /// <exception cref="ArgumentException">Thrown is the <paramref name="connectionString"/> is null or whitespace.</exception>
    public static IServiceCollection AddSqlite(this IServiceCollection services, string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        return services;
    }
}
