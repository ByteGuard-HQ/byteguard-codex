namespace ByteGuard.Codex.Core.Models;

public record AsvsVersionDetails
{
    public Guid Id { get; init; }
    public string VersionNumber { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public IReadOnlyList<AsvsChapterDetails> Chapters { get; init; }
}
