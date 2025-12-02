namespace ByteGuard.Codex.Core.Entities;

public sealed class AsvsRequirement : BaseRequirement
{
    public Guid AsvsSectionId { get; set; }
    public AsvsSection AsvsSection { get; set; }
}
