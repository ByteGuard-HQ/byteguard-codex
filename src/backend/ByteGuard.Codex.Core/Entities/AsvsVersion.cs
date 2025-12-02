namespace ByteGuard.Codex.Core.Entities;

public sealed class AsvsVersion
{
    public Guid Id { get; set; } // DB primary key - surrogate key

    public string VersionNumber { get; set; } // e.g. 5.0.0
    public string Name { get; set; } // e.g. OWASP ASVS v5.0.0
    public string? Description { get; set; }
    public List<AsvsChapter> AsvsChapters { get; set; } = new();
}
