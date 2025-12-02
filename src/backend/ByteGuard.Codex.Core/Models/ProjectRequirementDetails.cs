using ByteGuard.Codex.Core.Enums;

namespace ByteGuard.Codex.Core.Models;

public record ProjectRequirementDetails
{
    public Guid Id { get; set; }
    public RequirementDetails? Requirement { get; set; }
    public RequirementStatus Status { get; set; }
    public string? Notes { get; set; }
    public string? EvidenceLink { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}
