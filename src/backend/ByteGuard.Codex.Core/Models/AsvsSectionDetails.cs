using ByteGuard.Codex.Core.ValueObjects;

namespace ByteGuard.Codex.Core.Models;

/// <summary>
/// ASVS section details.
/// </summary>
public record AsvsSectionDetails
{
    /// <summary>
    /// Section identifier.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// ASVS short code used to identifiy and reference the section.
    /// </summary>
    /// <remarks>
    /// E.g. <c>"V1.1"</c>.
    /// </remarks>
    public required string Code { get; init; }

    /// <summary>
    /// Section ordinal, defining the order within the chapter.
    /// </summary>
    public required int Ordinal { get; init; }

    /// <summary>
    /// Section name.
    /// </summary>
    /// <remarks>
    /// E.g. "Encoding and Sanitization Architecture"
    /// </remarks>
    public required string Name { get; init; }

    /// <summary>
    /// Collection of all requirements within this section.
    /// </summary>
    public required IReadOnlyList<AsvsRequirementDetails> Requirements { get; init; }
}
