using ByteGuard.Codex.Core.ValueObjects;

namespace ByteGuard.Codex.Core.Entities;

/// <summary>
/// ASVS section.
/// </summary>
public sealed class AsvsSection
{
    /// <summary>
    /// Section identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ASVS short code used to identifiy and reference the section.
    /// </summary>
    /// <remarks>
    /// E.g. <c>"V1.1"</c>.
    /// </remarks>
    public required AsvsCode Code { get; set; }

    /// <summary>
    /// Section ordinal, defining the order within the chapter.
    /// </summary>
    public required int Ordinal { get; set; }

    /// <summary>
    /// Section name.
    /// </summary>
    /// <remarks>
    /// E.g. "Encoding and Sanitization Architecture"
    /// </remarks>
    public required string Name { get; set; }

    /// <summary>
    /// Identifier of the chapter in which this section belongs.
    /// </summary>
    public required Guid AsvsChapterId { get; set; }

    /// <summary>
    /// Chapter in which this section belongs.
    /// </summary>
    public AsvsChapter AsvsChapter { get; set; } = default!;

    /// <summary>
    /// Collection of all requirements within this section.
    /// </summary>
    public ICollection<AsvsRequirement> AsvsRequirements { get; set; } = default!;
}
