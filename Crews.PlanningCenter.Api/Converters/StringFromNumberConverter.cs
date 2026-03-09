using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.Converters;

/// <summary>
/// Converts a JSON number or string token to a <see cref="string"/>.
/// </summary>
public class StringFromNumberConverter : JsonConverter<string>
{
    /// <summary>
    /// Reads a JSON number or string token and returns its value as a <see cref="string"/>.
    /// Integer values are preserved without a decimal point; floating-point values use
    /// <see cref="double"/> formatting.
    /// </summary>
    /// <inheritdoc/>
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.TryGetInt64(out long intValue)
                ? intValue.ToString()
                : reader.GetDouble().ToString();
        }
        else if (reader.TokenType == JsonTokenType.String)
        {
            return reader.GetString() ?? string.Empty;
        }
        else
        {
            throw new JsonException($"Unexpected token parsing string. Expected Number or String, got {reader.TokenType}.");
        }
    }

    /// <summary>
    /// Writes <paramref name="value"/> as a JSON string token.
    /// </summary>
    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}
