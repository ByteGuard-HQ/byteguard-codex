using System.Text.Json;
using System.Text.Json.Serialization;
using ByteGuard.Codex.Core.ValueObjects;

namespace ByteGuard.Codex.Api.Converters;

/// <summary>
/// JSON converter for the <see cref="AsvsCode"/> type.
/// </summary>
public class AsvsCodeJsonConverter : JsonConverter<AsvsCode?>
{
    /// <inheritdoc/>
    public override AsvsCode? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var stringValue = reader.GetString();
        if (string.IsNullOrWhiteSpace(stringValue)) return null;

        return AsvsCode.Parse(stringValue);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, AsvsCode? value, JsonSerializerOptions options)
    {
        if (value is null) writer.WriteNullValue();

        writer.WriteStringValue(value.ToString());
    }
}
