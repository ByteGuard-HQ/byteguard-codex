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
    public required Guid Id { get; init; }

    /// <summary>
    /// ASVS short code used to identifiy and reference the requirement.
    /// </summary>
    /// <remarks>
    /// E.g. <c>"V1.1.1"</c>.
    /// </remarks>
    public required string Code { get; init; }

    /// <summary>
    /// Requirement ordinal, defining the order within the section.
    /// </summary>
    public required int Ordinal { get; init; }

    /// <summary>
    /// Requirement description.
    /// </summary>
    public required string Description { get; init; }

    /// <summary>
    /// Appropriate ASVS level for the requirement.
    /// </summary>
    public required AsvsLevel Level { get; init; }
}
