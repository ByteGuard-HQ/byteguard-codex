using ByteGuard.Codex.Core.Abstractions.DataStorage;
using ByteGuard.Codex.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ByteGuard.Codex.Infrastructure.Sqlite;

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
}
