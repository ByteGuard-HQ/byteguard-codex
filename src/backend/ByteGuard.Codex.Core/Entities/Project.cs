using ByteGuard.Codex.Core.Enums;

namespace ByteGuard.Codex.Core.Entities;

public sealed class Project
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Owner { get; set; }
    public ProjectStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ArchivedAt { get; set; }

    public Guid AsvsVersionId { get; set; }
    public AsvsVersion AsvsVersion { get; set; }
    public List<ProjectRequirement> Requirements { get; set; } = new();

    public Project(Guid id)
    {
        Id = id;
    }
}
