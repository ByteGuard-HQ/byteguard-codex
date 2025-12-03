namespace ByteGuard.Codex.Core.Models;

/// <summary>
/// ASVS version metadata.
/// </summary>
public record AsvsVersionMetadata
{
    /// <summary>
    /// Version identifier.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// ASVS version number.
    /// </summary>
    /// <remarks>
    /// E.g. <c>"0.1.0"</c>, <c>"1.0.0"</c> or <c>"4.0.3"</c>.
    /// </remarks>
    public required string VersionNumber { get; init; }

    /// <summary>
    /// ASVS version name.
    /// </summary>
    /// <remarks>
    /// E.g. "Organization XYZ ASVS" or "OWASP ASVS v5.0.0"
    /// </remarks>
    public required string Name { get; init; }

    /// <summary>
    /// Optional ASVS version description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Whether this ASVS version is read only.
    /// </summary>
    /// <remarks>
    /// If <c>true</c>, no modifications may occur on the version, nor chapters, sections and requirements within the version.
    /// This is set to <c>true</c> when importing OWASP ASVS versions.
    /// </remarks>
    public required bool IsReadOnly { get; init; }
}
