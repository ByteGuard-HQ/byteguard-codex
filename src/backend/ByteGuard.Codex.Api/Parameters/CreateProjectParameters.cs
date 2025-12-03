using System.ComponentModel.DataAnnotations;

namespace ByteGuard.Codex.Api.Parameters;

/// <summary>
/// Parameters to use whenever creating a project.
/// </summary>
public class CreateProjectParameters
{
    /// <summary>
    /// Project title.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public required string Title { get; set; }

    /// <summary>
    /// Optional project description.
    /// </summary>
    [MaxLength(2000)]
    public string? Description { get; init; }

    /// <summary>
    /// Project owner.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public required string Owner { get; init; }

    /// <summary>
    /// The ASVS version this project is comitting to.
    /// </summary>
    [Required]
    public required Guid AsvsVersionId { get; init; }
}
