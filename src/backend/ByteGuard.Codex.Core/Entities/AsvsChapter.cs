using ByteGuard.Codex.Core.ValueObjects;

namespace ByteGuard.Codex.Core.Entities;

/// <summary>
/// ASVS chapter.
/// </summary>
public sealed class AsvsChapter
{
    /// <summary>
    /// Chapter identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ASVS short code used to identifiy and reference the chapter.
    /// </summary>
    /// <remarks>
    /// E.g. <c>"V1"</c>.
    /// </remarks>
    public AsvsCode Code { get; set; }

    /// <summary>
    /// Chapter ordinal, defining the order within the ASVS version.
    /// </summary>
    public int Ordinal { get; set; }

    /// <summary>
    /// Chapter title.
    /// </summary>
    /// <remarks>
    /// E.g. "Encoding and Sanitization".
    /// </remarks>
    public string Title { get; set; }

    /// <summary>
    /// Optional chapter description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Identifier of the ASVS version in which this chapter belongs.
    /// </summary>
    public Guid AsvsVersionId { get; set; }

    /// <summary>
    /// ASVS version in which this chapter belongs.
    /// </summary>
    public AsvsVersion AsvsVersion { get; set; }

    /// <summary>
    /// Collection of all sections within this chapter.
    /// </summary>
    public ICollection<AsvsSection> AsvsSections { get; set; }
}
