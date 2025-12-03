namespace ByteGuard.Codex.Infrastructure.Sqlite.Seeding;

internal record AsvsJsonRoot
{
    public required string Name { get; set; }
    public required string ShortName { get; set; }
    public required string Version { get; set; }
    public string? Description { get; set; }
    public List<AsvsJsonChapter> Requirements { get; set; } = new();
}

internal record AsvsJsonChapter
{
    public required string Shortcode { get; set; }
    public required int Ordinal { get; set; }
    public required string ShortName { get; set; }
    public required string Name { get; set; }
    public List<AsvsJsonSection> Items { get; set; } = new();
}

internal record AsvsJsonSection
{
    public required string Shortcode { get; set; }
    public required int Ordinal { get; set; }
    public required string Name { get; set; }
    public List<AsvsJsonRequirement> Items { get; set; } = new();
}

internal record AsvsJsonRequirement
{
    public required string Shortcode { get; set; }
    public required int Ordinal { get; set; }
    public required string Description { get; set; }
    public required string L { get; set; }
}
