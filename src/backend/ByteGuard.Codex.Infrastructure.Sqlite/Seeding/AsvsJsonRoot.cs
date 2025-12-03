namespace ByteGuard.Codex.Infrastructure.Sqlite.Seeding;

internal record AsvsJsonRoot
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Version { get; set; }
    public string Description { get; set; }
    public List<AsvsJsonChapter> Requirements { get; set; }
}

internal record AsvsJsonChapter
{
    public string Shortcode { get; set; }
    public int Ordinal { get; set; }
    public string ShortName { get; set; }
    public string Name { get; set; }
    public List<AsvsJsonSection> Items { get; set; }
}

internal record AsvsJsonSection
{
    public string Shortcode { get; set; }
    public int Ordinal { get; set; }
    public string Name { get; set; }
    public List<AsvsJsonRequirement> Items { get; set; }
}

internal record AsvsJsonRequirement
{
    public string Shortcode { get; set; }
    public int Ordinal { get; set; }
    public string Description { get; set; }
    public string L { get; set; }
}
