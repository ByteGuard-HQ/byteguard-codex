using ByteGuard.Codex.Core.Enums;

namespace ByteGuard.Codex.Core.Models;

public record RequirementDetails
{
    public Guid Id { get; init; }
    public string Code { get; init; }
    public int Ordinal { get; init; }
    public string Description { get; init; }
    public AsvsLevel Level { get; init; }
}
