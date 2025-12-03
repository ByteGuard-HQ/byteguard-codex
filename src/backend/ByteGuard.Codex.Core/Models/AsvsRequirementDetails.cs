using ByteGuard.Codex.Core.Enums;
using ByteGuard.Codex.Core.ValueObjects;

namespace ByteGuard.Codex.Core.Models;

/// <summary>
/// ASVS requirement details.
/// </summary>
public record AsvsRequirementDetails
{
    /// <summary>
    /// Requirement identifier.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// ASVS short code used to identifiy and reference the requirement.
    /// </summary>
    /// <remarks>
    /// E.g. <c>"V1.1.1"</c>.
    /// </remarks>
    public AsvsCode Code { get; init; }

    /// <summary>
    /// Requirement ordinal, defining the order within the section.
    /// </summary>
    public int Ordinal { get; init; }

    /// <summary>
    /// Requirement description.
    /// </summary>
    public string Description { get; init; }

    /// <summary>
    /// Appropriate ASVS level for the requirement.
    /// </summary>
    public AsvsLevel Level { get; init; }
}
