namespace ByteGuard.Codex.Core.Models;

public record AsvsVersionMetadata
{
    public Guid Id { get; init; }
    public string VersionNumber { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
}
