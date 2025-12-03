using System.ComponentModel.DataAnnotations;

namespace ByteGuard.Codex.Api.Parameters;

/// <summary>
/// Parameters to use whenever creating or updating an ASVS section.
/// </summary>
public record CreateAsvsSectionParamters
{
    /// <summary>
    /// Section name.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public required string Name { get; init; }
}
