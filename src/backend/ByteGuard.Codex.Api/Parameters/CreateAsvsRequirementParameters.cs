using System.ComponentModel.DataAnnotations;
using ByteGuard.Codex.Core.Enums;

namespace ByteGuard.Codex.Api.Parameters;

/// <summary>
/// Parameters to use whenever creating or updating an ASVS requirement.
/// </summary>
/// <value></value>
public record CreateAsvsRequirementParameters
{
    /// <summary>
    /// Requirement description.
    /// </summary>
    [Required]
    [MaxLength(1500)]
    public required string Description { get; init; }

    /// <summary>
    /// Appropriate ASVS level for the requirement.
    /// </summary>
    [Required]
    public required AsvsLevel Level { get; init; }
}
