using ByteGuard.Codex.Core.Enums;

namespace ByteGuard.Codex.Core.Models;

/// <summary>
/// Project metadata.
/// </summary>
public record ProjectMetadata
{
    /// <summary>
    /// Project identifier.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Project title.
    /// </summary>
    public string Title { get; init; }

    /// <summary>
    /// Optional project description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Project owner.
    /// </summary>
    public string Owner { get; init; }

    /// <summary>
    /// Current status of the project.
    /// </summary>
    public ProjectStatus Status { get; init; }

    /// <summary>
    /// Date and time of project creation.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Date and time of project archival.
    /// </summary>
    public DateTime? ArchivedAt { get; init; }

    /// <summary>
    /// Identifier of the ASVS version this project is comitting to.
    /// </summary>
    public Guid AsvsVersionId { get; init; }
}
