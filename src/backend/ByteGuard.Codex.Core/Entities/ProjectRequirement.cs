using ByteGuard.Codex.Core.Enums;

namespace ByteGuard.Codex.Core.Entities;

public class ProjectRequirement
{
    public Guid Id { get; set; }
    public RequirementStatus Status { get; set; }
    public AsvsRequirement? AsvsRequirement { get; set; }
    public CustomRequirement? CustomRequirement { get; set; }

    public string? Notes { get; set; }
    public string? EvidenceLink { get; set; }

    public DateTime LastUpdatedAt { get; set; }
}
