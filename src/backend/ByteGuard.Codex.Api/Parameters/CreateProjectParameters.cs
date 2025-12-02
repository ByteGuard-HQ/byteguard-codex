using System.ComponentModel.DataAnnotations;

namespace ByteGuard.Codex.Api.Parameters;

public class CreateProjectParameters
{
    /// <summary>
    /// Title.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    /// <summary>
    /// Optional description.
    /// </summary>
    [MaxLength(2000)]
    public string? Description { get; set; }

    /// <summary>
    /// Project owner.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Owner { get; set; }

    /// <summary>
    /// The ASVS version this project is comitting to.
    /// </summary>
    [Required]
    public Guid AsvsVersionId { get; set; }
}
