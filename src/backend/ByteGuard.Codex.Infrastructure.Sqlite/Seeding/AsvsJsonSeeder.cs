using System.Text.Json;
using ByteGuard.Codex.Core.Entities;
using ByteGuard.Codex.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace ByteGuard.Codex.Infrastructure.Sqlite.Seeding;

public static class AsvsJsonSeeder
{
    public static void SeedAsvs(DbContext context, bool _, string version)
    {
        SeedInternalAsync(context, _, version, CancellationToken.None).GetAwaiter().GetResult();
    }

    public static async Task SeedAsvsAsync(DbContext context, bool _, string version, CancellationToken cancellationToken)
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

        var asvsVersion = new AsvsVersion();
        asvsVersion.VersionNumber = root.Version;
        asvsVersion.Name = $"OWASP ASVS {root.Version}";
        asvsVersion.Description = root.Description;

        await context.Set<AsvsVersion>().AddAsync(asvsVersion);
        await context.SaveChangesAsync();

        var chapters = new Dictionary<string, AsvsChapter>();
        var sections = new Dictionary<string, AsvsSection>();

        foreach (var c in root.Requirements)
        {
            if (!chapters.TryGetValue(c.Shortcode, out var chapter))
            {
                chapter = new AsvsChapter();
                chapter.Code = c.Shortcode;
                chapter.Ordinal = c.Ordinal;
                chapter.Title = c.Name;
                chapter.AsvsVersionId = asvsVersion.Id;

                chapters.Add(chapter.Code, chapter);
                await context.Set<AsvsChapter>().AddAsync(chapter);
            }

            foreach (var s in c.Items)
            {
                if (!sections.TryGetValue(s.Shortcode, out var section))
                {
                    section = new AsvsSection();
                    section.Code = s.Shortcode;
                    section.Ordinal = s.Ordinal;
                    section.Name = s.Name;
                    section.AsvsChapter = chapter;

                    sections.Add(section.Code, section);
                    await context.Set<AsvsSection>().AddAsync(section);
                }

                foreach (var r in s.Items)
                {
                    var requirement = new AsvsRequirement();
                    requirement.Code = r.Shortcode;
                    requirement.Ordinal = r.Ordinal;
                    requirement.Description = r.Description;
                    requirement.Level = ParseLevel(r.L);
                    requirement.AsvsSection = section;

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

        var root = JsonSerializer.Deserialize<AsvsJsonRoot>(json);

        return root!;
    }
}
