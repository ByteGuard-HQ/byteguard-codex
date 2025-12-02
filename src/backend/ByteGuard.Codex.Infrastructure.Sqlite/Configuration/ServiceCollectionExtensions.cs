using ByteGuard.Codex.Core.Abstractions.DataStorage;
using ByteGuard.Codex.Core.Entities;
using ByteGuard.Codex.Core.Enums;
using ByteGuard.Codex.Infrastructure.Sqlite.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ByteGuard.Codex.Infrastructure.Sqlite.Configuration;

public static class ServiceCollectionExtensions
{
    private static List<string> SupportedAsvsVersion = ["5.0.0"];

    /// <summary>
    /// Add Sqlite dependencies to the service collection.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="connectionString">Connection string.</param>
    /// <exception cref="ArgumentException">Thrown is the <paramref name="connectionString"/> is null or whitespace.</exception>
    public static IServiceCollection AddCodexSqliteStorage(this IServiceCollection services, string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.AddDbContext<ICodexDbContext, CodexDbContext>(options =>
        {
            options.UseSqlite(connectionString, opts => opts.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            options.UseSeeding((context, _) =>
            {
                foreach (var version in SupportedAsvsVersion)
                {
                    AsvsJsonSeeder.SeedAsvs(context, _, version);
                }
            });
            options.UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
                foreach (var version in SupportedAsvsVersion)
                {
                    await AsvsJsonSeeder.SeedAsvsAsync(context, _, version, cancellationToken);
                }
            });
        });

        return services;
    }
}
