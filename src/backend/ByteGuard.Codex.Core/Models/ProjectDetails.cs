using ByteGuard.Codex.Core.Enums;

namespace ByteGuard.Codex.Core.Models;

/// <summary>
/// Project details.
/// </summary>
public record ProjectDetails
{
    /// <summary>
    /// Project identifier.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Project title.
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// Optional project description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Project owner.
    /// </summary>
    public required string Owner { get; init; }

    /// <summary>
    /// Current status of the project.
    /// </summary>
    public required ProjectStatus Status { get; init; }

    /// <summary>
    /// Date and time of project creation.
    /// </summary>
    public required DateTime CreatedAt { get; init; }

    /// <summary>
    /// Date and time of project archival.
    /// </summary>
    public DateTime? ArchivedAt { get; init; }

    /// <summary>
    /// Identifier of the ASVS version this project is comitting to.
    /// </summary>
    public required Guid AsvsVersionId { get; init; }

    /// <summary>
    /// Collection of all project requirements this project has comitted to.
    /// </summary>
    public IReadOnlyList<ProjectRequirementDetails> Requirements { get; init; } = default!;
}
