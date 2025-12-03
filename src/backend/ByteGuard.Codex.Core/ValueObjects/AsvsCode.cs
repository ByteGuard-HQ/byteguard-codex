namespace ByteGuard.Codex.Core.ValueObjects;

/// <summary>
/// ASVS short code value object.
/// </summary>
public readonly record struct AsvsCode : IComparable<AsvsCode>, IEquatable<AsvsCode>
{
    /// <summary>
    /// The chapter number of the code.
    /// </summary>
    public int Chapter { get; }

    /// <summary>
    /// The section number of the code.
    /// </summary>
    public int? Section { get; }

    /// <summary>
    /// The requirement number of the code.
    /// </summary>
    public int? Requirement { get; }

    /// <summary>
    /// Whether the short code only consists of a chapter number.
    /// </summary>
    public bool IsChapterOnly => Section is null && Requirement is null;

    /// <summary>
    /// Whether the short code represents a section.
    /// </summary>
    public bool IsSection => Section is not null && Requirement is null;

    /// <summary>
    /// Whether the short code represents a requirement.
    /// </summary>
    public bool IsRequirement => Requirement is not null;

    /// <summary>
    /// Create a new ASVS code instance.
    /// </summary>
    /// <param name="chapter">Chapter number.</param>
    /// <param name="section">Section number.</param>
    /// <param name="requirement">Requirement number.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if any of the provided numbers are invalid.</exception>
    public AsvsCode(int chapter, int? section = null, int? requirement = null)
    {
        if (chapter <= 0) throw new ArgumentOutOfRangeException(nameof(chapter));
        if (section <= 0) throw new ArgumentOutOfRangeException(nameof(section));
        if (requirement <= 0) throw new ArgumentOutOfRangeException(nameof(requirement));

        Chapter = chapter;
        Section = section;
        Requirement = requirement;
    }

    /// <summary>
    /// Convert the <see cref="AsvsCode"/> instance to its string representation.
    /// </summary>
    /// <remarks>
    /// E.g. <c>"V1"</c> for chapters, <c>"V1.1"</c> for section, and <c>"V1.1.1"</c> for requirements.
    /// </remarks>
    /// <returns>String representation of the <see cref="AsvsCode"/>.</returns>
    public override string ToString()
    {
        if (Section is null) return $"V{Chapter}";
        if (Requirement is null) return $"V{Chapter}.{Section}";
        return $"V{Chapter}.{Section}.{Requirement}";
    }

    /// <summary>
    /// Parse the given input to an <see cref="AsvsCode"/> instance.
    /// </summary>
    /// <param name="input">String representation of a short code.</param>
    /// <returns><see cref="AsvsCode"/> instance if the short code representation is valid.</returns>
    /// <exception cref="FormatException">Thrown if the input does not match the expected format of an ASVS short code.</exception>
    public static AsvsCode Parse(string input)
    {
        if (!TryParse(input, out var code))
            throw new FormatException($"'{input}' is not a valid ASVS code.");

        return code;
    }

    /// <summary>
    /// Try to parse a string representation of a short code into an <see cref="AsvsCode"/> instance.
    /// </summary>
    /// <param name="input">String representation of a short code.</param>
    /// <param name="asvsCode">On return, contains the <see cref="AsvsCode"/> instance if successfully parsed.</param>
    /// <returns><c>true</c> if parse was successfull, <c>false</c> otherwise.</returns>
    public static bool TryParse(string? input, out AsvsCode asvsCode)
    {
        asvsCode = default;

        if (string.IsNullOrWhiteSpace(input))
            return false;

        input = input.Trim();

        if (!input.StartsWith("V", StringComparison.OrdinalIgnoreCase))
            return false;

        var parts = input[1..].Split('.', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length is < 1 or > 3)
            return false;

        if (!int.TryParse(parts[0], out var chapter) || chapter <= 0)
            return false;

        int? section = null;
        int? requirement = null;

        if (parts.Length >= 2)
        {
            if (!int.TryParse(parts[1], out var s) || s <= 0)
                return false;
            section = s;
        }

        if (parts.Length == 3)
        {
            if (!int.TryParse(parts[2], out var r) || r <= 0)
                return false;
            requirement = r;
        }

        asvsCode = new AsvsCode(chapter, section, requirement);
        return true;
    }

    /// <inheritdoc />
    public int CompareTo(AsvsCode other)
    {
        var c = Chapter.CompareTo(other.Chapter);
        if (c != 0) return c;

        // Null sections come before non-null: V1 < V1.1
        c = Nullable.Compare(Section, other.Section);
        if (c != 0) return c;

        // Null requirements come before non-null: V1.1 < V1.1.1
        return Nullable.Compare(Requirement, other.Requirement);
    }
}
