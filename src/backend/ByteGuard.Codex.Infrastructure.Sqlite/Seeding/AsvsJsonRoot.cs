namespace ByteGuard.Codex.Infrastructure.Sqlite.Seeding;

public class AsvsJsonRoot
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Version { get; set; }
    public string Description { get; set; }
    public List<AsvsJsonChapter> Requirements { get; set; }
}

public class AsvsJsonChapter
{
    public string Shortcode { get; set; }
    public int Ordinal { get; set; }
    public string ShortName { get; set; }
    public string Name { get; set; }
    public List<AsvsJsonSection> Items { get; set; }
}

public class AsvsJsonSection
{
    public string Shortcode { get; set; }
    public int Ordinal { get; set; }
    public string Name { get; set; }
    public List<AsvsJsonRequirement> Items { get; set; }
}

public class AsvsJsonRequirement
{
    public string Shortcode { get; set; }
    public int Ordinal { get; set; }
    public string Description { get; set; }
    public string L { get; set; }
}
