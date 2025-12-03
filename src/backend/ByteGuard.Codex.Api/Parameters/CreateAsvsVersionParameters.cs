using System.ComponentModel.DataAnnotations;

namespace ByteGuard.Codex.Api.Parameters;

/// <summary>
/// Parameters to use whenever creating or updating an ASVS version.
/// </summary>
public record CreateAsvsVersionParameters
{
    /// <summary>
    /// ASVS version number.
    /// </summary>
    /// <remarks>
    /// E.g. <c>"0.1.0"</c>, <c>"1.0.0"</c> or <c>"4.0.3"</c>.
    /// </remarks>
    [Required]
    [MaxLength(10)]
    public string VersionNumber { get; init; }

    /// <summary>
    /// ASVS version name.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; init; }

    /// <summary>
    /// Optional ASVS version description.
    /// </summary>
    [MaxLength(2000)]
    public string? Description { get; init; }
}
