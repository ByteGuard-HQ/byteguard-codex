namespace ByteGuard.Codex.Core.Entities;

public sealed class AsvsChapter
{
    public Guid Id { get; set; } // DB primary key - surrogate key

    public string Code { get; set; } // e.g. V1, V2, etc.
    public int Ordinal { get; set; }
    public string Title { get; set; } // e.g. "Encoding and Sanitization"
    public string? Description { get; set; }

    public Guid AsvsVersionId { get; set; }
    public AsvsVersion AsvsVersion { get; set; }
    public ICollection<AsvsSection> AsvsSections { get; set; }
}
