namespace ByteGuard.Codex.Core.Entities;

/// <summary>
/// ASVS version.
/// </summary>
public sealed class AsvsVersion
{
    /// <summary>
    /// Version identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ASVS version number.
    /// </summary>
    /// <remarks>
    /// E.g. <c>"0.1.0"</c>, <c>"1.0.0"</c> or <c>"4.0.3"</c>.
    /// </remarks>
    public string VersionNumber { get; set; }

    /// <summary>
    /// ASVS version name.
    /// </summary>
    /// <remarks>
    /// E.g. "Organization XYZ ASVS" or "OWASP ASVS v5.0.0"
    /// </remarks>
    public string Name { get; set; }

    /// <summary>
    /// Optional ASVS version description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Whether this ASVS version is read only.
    /// </summary>
    /// <remarks>
    /// If <c>true</c>, no modifications may occur on the version, nor chapters, sections and requirements within the version.
    /// This is set to <c>true</c> when importing OWASP ASVS versions.
    /// </remarks>
    public bool IsReadOnly { get; set; } = false;

    /// <summary>
    /// Collection of all chapters within this version.
    /// </summary>
    public List<AsvsChapter> AsvsChapters { get; set; } = new();
}
