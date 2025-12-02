namespace ByteGuard.Codex.Core.Entities;

public sealed class AsvsSection
{
    public Guid Id { get; set; } // DB primary key - surrogate key

    public string Code { get; set; } // e.g. V1.1, V1.2, etc.
    public int Ordinal { get; set; }
    public string Name { get; set; } // e.g. "Encoding and Sanitization Architecture"

    public Guid AsvsChapterId { get; set; }
    public AsvsChapter AsvsChapter { get; set; }
    public ICollection<AsvsRequirement> AsvsRequirements { get; set; }
}
