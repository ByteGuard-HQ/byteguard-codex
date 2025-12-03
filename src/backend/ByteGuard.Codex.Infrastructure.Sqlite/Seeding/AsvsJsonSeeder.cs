using System.Text.Json;
using ByteGuard.Codex.Core.Entities;
using ByteGuard.Codex.Core.Enums;
using ByteGuard.Codex.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace ByteGuard.Codex.Infrastructure.Sqlite.Seeding;

internal static class AsvsJsonSeeder
{
    internal static void SeedAsvs(DbContext context, bool _, string version)
    {
        SeedInternalAsync(context, _, version, CancellationToken.None).GetAwaiter().GetResult();
    }

    internal static async Task SeedAsvsAsync(DbContext context, bool _, string version, CancellationToken cancellationToken)
    {
        await SeedInternalAsync(context, _, version, cancellationToken);
    }

    private static async Task SeedInternalAsync(DbContext context, bool _, string version, CancellationToken cancellationToken)
    {
        var hasVersion = await context.Set<AsvsVersion>().AnyAsync(x => x.VersionNumber.Equals(version));
        if (hasVersion) return;

        var basePath = AppContext.BaseDirectory;
        var fullPath = Path.Combine(basePath, "Seeding", $"owasp_asvs_{version}.json");

        var root = await GetJsonContent(fullPath, cancellationToken);

        var asvsVersion = new AsvsVersion
        {
            VersionNumber = root.Version,
            Name = $"OWASP ASVS {root.Version}",
            Description = root.Description,
            IsReadOnly = true
        };

        await context.Set<AsvsVersion>().AddAsync(asvsVersion);
        await context.SaveChangesAsync();

        var chapters = new Dictionary<string, AsvsChapter>();
        var sections = new Dictionary<string, AsvsSection>();

        foreach (var c in root.Requirements)
        {
            if (!chapters.TryGetValue(c.Shortcode, out var chapter))
            {
                chapter = new AsvsChapter
                {
                    Code = AsvsCode.Parse(c.Shortcode),
                    Ordinal = c.Ordinal,
                    Title = c.Name,
                    AsvsVersionId = asvsVersion.Id
                };

                chapters.Add(chapter.Code.ToVersionString(), chapter);
                await context.Set<AsvsChapter>().AddAsync(chapter);
            }

            foreach (var s in c.Items)
            {
                if (!sections.TryGetValue(s.Shortcode, out var section))
                {
                    section = new AsvsSection
                    {
                        Code = AsvsCode.Parse(s.Shortcode),
                        Ordinal = s.Ordinal,
                        Name = s.Name,
                        AsvsChapterId = chapter.Id
                    };

                    sections.Add(section.Code.ToVersionString(), section);
                    await context.Set<AsvsSection>().AddAsync(section);
                }

                foreach (var r in s.Items)
                {
                    var requirement = new AsvsRequirement
                    {
                        Code = AsvsCode.Parse(r.Shortcode),
                        Ordinal = r.Ordinal,
                        Description = r.Description,
                        Level = ParseLevel(r.L),
                        AsvsSectionId = section.Id
                    };

                    await context.Set<AsvsRequirement>().AddAsync(requirement);
                }
            }
        }

        await context.SaveChangesAsync();
    }

    private static AsvsLevel ParseLevel(string level)
    {
        return level.Trim().ToUpperInvariant() switch
        {
            "1" => AsvsLevel.L1,
            "2" => AsvsLevel.L2,
            "3" => AsvsLevel.L3,
            _ => throw new InvalidOperationException("Unknown ASVS level")
        };
    }

    private static async Task<AsvsJsonRoot> GetJsonContent(string path, CancellationToken cancellationToken)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"ASVS JSON seed file not found at '{path}'. Ensure it is marked as Content with CopyToOutputDirectory.");
        }

        var json = await File.ReadAllTextAsync(path, cancellationToken);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var root = JsonSerializer.Deserialize<AsvsJsonRoot>(json, options);

        return root!;
    }
}
