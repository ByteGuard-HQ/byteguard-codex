using System.ComponentModel.DataAnnotations;

namespace ByteGuard.Codex.Api.Parameters;

/// <summary>
/// Parameters to use whenever creating or updating an ASVS chapter.
/// </summary>
public record CreateAsvsChapterParameters
{
    /// <summary>
    /// Chapter title.
    /// </summary>
    [Required]
    [MaxLength(150)]
    public string Title { get; init; }

    /// <summary>
    /// Optional chapter description.
    /// </summary>
    [MaxLength(2000)]
    public string? Description { get; init; }
}
