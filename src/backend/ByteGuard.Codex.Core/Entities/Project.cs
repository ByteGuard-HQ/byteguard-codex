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
    public string Title { get; set; }

    /// <summary>
    /// Optional project description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Project owner.
    /// </summary>
    public string Owner { get; set; }

    /// <summary>
    /// Current status of the project.
    /// </summary>
    public ProjectStatus Status { get; set; }

    /// <summary>
    /// Date and time of project creation.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date and time of project archival.
    /// </summary>
    public DateTime? ArchivedAt { get; set; }

    /// <summary>
    /// Identifier of the ASVS version this project is comitting to.
    /// </summary>
    public Guid AsvsVersionId { get; set; }

    /// <summary>
    /// ASVS version this project is comitting to.
    /// </summary>
    public AsvsVersion AsvsVersion { get; set; }

    /// <summary>
    /// Collection of all project requirements this project has comitted to.
    /// </summary>
    public List<ProjectRequirement> Requirements { get; set; } = new();
}
