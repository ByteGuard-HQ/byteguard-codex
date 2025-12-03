using ByteGuard.Codex.Core.Enums;
using ByteGuard.Codex.Core.ValueObjects;

namespace ByteGuard.Codex.Core.Entities;

/// <summary>
/// ASVS requirement.
/// </summary>
public sealed class AsvsRequirement
{
    /// <summary>
    /// Requirement identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ASVS short code used to identifiy and reference the requirement.
    /// </summary>
    /// <remarks>
    /// E.g. <c>"V1.1.1"</c>.
    /// </remarks>
    public required AsvsCode Code { get; set; }

    /// <summary>
    /// Requirement ordinal, defining the order within the section.
    /// </summary>
    public required int Ordinal { get; set; }

    /// <summary>
    /// Requirement description.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Appropriate ASVS level for the requirement.
    /// </summary>
    public required AsvsLevel Level { get; set; }

    /// <summary>
    /// Identifier of the section in which this requirement belongs.
    /// </summary>
    public required Guid AsvsSectionId { get; set; }

    /// <summary>
    /// Section in which this requirement belongs.
    /// </summary>
    public AsvsSection AsvsSection { get; set; } = default!;
}
