using ByteGuard.Codex.Core.Enums;

namespace ByteGuard.Codex.Core.Entities;

/// <summary>
/// Project specific requirement.
/// </summary>
public class ProjectRequirement
{
    /// <summary>
    /// Project requirement identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Current status of this specific project requirement.
    /// </summary>
    public required RequirementStatus Status { get; set; }

    /// <summary>
    /// ASVS requirement this project requirement is linked to.
    /// </summary>
    public required AsvsRequirement AsvsRequirement { get; set; }

    /// <summary>
    /// Optional requirements notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Optional evidence link.
    /// </summary>
    public string? EvidenceLink { get; set; }

    /// <summary>
    /// Date and time of last update to the project requirement.
    /// </summary>
    public DateTime LastUpdatedAt { get; set; }
}
