using ByteGuard.Codex.Core.Abstractions.DataStorage;
using ByteGuard.Codex.Core.Entities;
using ByteGuard.Codex.Core.ValueObjects;
using ByteGuard.Codex.Infrastructure.Sqlite.ValueConverters;
using Microsoft.EntityFrameworkCore;

namespace ByteGuard.Codex.Infrastructure.Sqlite;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public class CodexDbContext : DbContext, ICodexDbContext
{
    public DbSet<AsvsVersion> AsvsVersions { get; set; }
    public DbSet<AsvsChapter> AsvsChapters { get; set; }
    public DbSet<AsvsSection> AsvsSections { get; set; }
    public DbSet<AsvsRequirement> AsvsRequirements { get; set; }
    public DbSet<ProjectRequirement> ProjectRequirements { get; set; }
    public DbSet<Project> Projects { get; set; }

    public CodexDbContext(DbContextOptions<CodexDbContext> options)
        : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<AsvsCode>()
            .HaveConversion<AsvsCodeConverter>();
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
