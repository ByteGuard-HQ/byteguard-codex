using ByteGuard.Codex.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ByteGuard.Codex.Core.Abstractions.DataStorage;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public interface ICodexDbContext
{
    public DbSet<AsvsVersion> AsvsVersions { get; set; }
    public DbSet<AsvsChapter> AsvsChapters { get; set; }
    public DbSet<AsvsSection> AsvsSections { get; set; }
    public DbSet<AsvsRequirement> AsvsRequirements { get; set; }
    public DbSet<ProjectRequirement> ProjectRequirements { get; set; }
    public DbSet<Project> Projects { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
