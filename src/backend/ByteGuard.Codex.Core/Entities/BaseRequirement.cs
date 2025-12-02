using ByteGuard.Codex.Core.Enums;

namespace ByteGuard.Codex.Core.Entities;

public abstract class BaseRequirement
{
    public Guid Id { get; set; } // DB primary key - surrogate key
    public string Code { get; set; } // e.g. V1.1.1, V1.1.2, etc.
    public int Ordinal { get; set; }
    public string Description { get; set; }
    public AsvsLevel Level { get; set; }
}
