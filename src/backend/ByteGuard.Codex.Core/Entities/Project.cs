using ByteGuard.Codex.Core.Enums;

namespace ByteGuard.Codex.Core.Entities;

/// <summary>
/// Project definition.
/// </summary>
public sealed class Project
{
    /// <summary>
    /// Project identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Project title.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Optional project description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Project owner.
    /// </summary>
    public required string Owner { get; set; }

    /// <summary>
    /// Current status of the project.
    /// </summary>
    public required ProjectStatus Status { get; set; }

    /// <summary>
    /// Date and time of project creation.
    /// </summary>
    public required DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date and time of project archival.
    /// </summary>
    public DateTime? ArchivedAt { get; set; }

    /// <summary>
    /// Identifier of the ASVS version this project is comitting to.
    /// </summary>
    public required Guid AsvsVersionId { get; set; }

    /// <summary>
    /// ASVS version this project is comitting to.
    /// </summary>
    public AsvsVersion AsvsVersion { get; set; } = default!;

    /// <summary>
    /// Collection of all project requirements this project has comitted to.
    /// </summary>
    public ICollection<ProjectRequirement> Requirements { get; set; } = default!;
}
