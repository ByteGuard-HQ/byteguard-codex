namespace ByteGuard.Codex.Core.Models;

public record AsvsChapterDetails
{
    public Guid Id { get; init; }
    public string Code { get; init; }
    public int Ordinal { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public IReadOnlyList<AsvsSectionDetails> Sections { get; init; }
}
