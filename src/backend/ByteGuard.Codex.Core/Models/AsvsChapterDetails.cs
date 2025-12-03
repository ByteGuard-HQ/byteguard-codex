using ByteGuard.Codex.Core.ValueObjects;

namespace ByteGuard.Codex.Core.Models;

/// <summary>
/// ASVS chapter details.
/// </summary>
public record AsvsChapterDetails
{
    /// <summary>
    /// Chapter identifier.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// ASVS short code used to identifiy and reference the chapter.
    /// </summary>
    /// <remarks>
    /// E.g. <c>"V1"</c>.
    /// </remarks>
    public AsvsCode Code { get; init; }

    /// <summary>
    /// Chapter ordinal, defining the order within the ASVS version.
    /// </summary>
    public int Ordinal { get; init; }

    /// <summary>
    /// Chapter title.
    /// </summary>
    /// <remarks>
    /// E.g. "Encoding and Sanitization".
    /// </remarks>
    public string Title { get; init; }

    /// <summary>
    /// Optional chapter description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Collection of all sections within this chapter.
    /// </summary>
    public IReadOnlyList<AsvsSectionDetails> Sections { get; init; }
}
