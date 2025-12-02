namespace ByteGuard.Codex.Core.Models;

public record AsvsSectionDetails
{
    public Guid Id { get; init; }
    public string Code { get; init; }
    public int Ordinal { get; init; }
    public string Name { get; init; }
    public IReadOnlyList<AsvsRequirementDetails> Requirements { get; init; }
}
