using ByteGuard.Codex.Core.Enums;

namespace ByteGuard.Codex.Core.Models;

/// <summary>
/// Project requirement details.
/// </summary>
public record ProjectRequirementDetails
{
    /// <summary>
    /// Project requirement identifier.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// ASVS requirement this project requirement is linked to.
    /// </summary>
    public required AsvsRequirementDetails Requirement { get; init; }

    /// <summary>
    /// Current status of this specific project requirement.
    /// </summary>
    public required RequirementStatus Status { get; init; }

    /// <summary>
    /// Optional requirements notes.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Optional evidence link.
    /// </summary>
    public string? EvidenceLink { get; init; }

    /// <summary>
    /// Date and time of last update to the project requirement.
    /// </summary>
    public DateTime LastUpdatedAt { get; init; }
}
