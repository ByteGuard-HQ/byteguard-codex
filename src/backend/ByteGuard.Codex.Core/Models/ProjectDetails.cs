using ByteGuard.Codex.Core.Enums;

namespace ByteGuard.Codex.Core.Models;

public record ProjectDetails
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public string Owner { get; init; }
    public ProjectStatus Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? ArchivedAt { get; init; }
    public Guid AsvsVersionId { get; init; }
    public IReadOnlyList<ProjectRequirementDetails> Requirements { get; init; }
}
