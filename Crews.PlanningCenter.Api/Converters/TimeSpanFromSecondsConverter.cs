using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.Converters;

/// <summary>
/// A JSON converter that converts a string or number representing seconds into a TimeSpan object, and converts a 
/// TimeSpan object back into a number of seconds when writing JSON.
/// </summary>
public class TimeSpanFromSecondsConverter : JsonConverter<TimeSpan>
{
    /// <summary>
    /// Reads a JSON value and converts it to a TimeSpan object. The JSON value can be either a string or a number representing seconds.
    /// </summary>
    /// <param name="reader">The JSON reader to read from.</param>
    /// <param name="typeToConvert">The type to convert to.</param>
    /// <param name="options">The JSON serializer options.</param>
    /// <returns>The converted TimeSpan value.</returns>
    /// <exception cref="JsonException">Thrown when the value cannot be converted to a TimeSpan.</exception>
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var str = reader.GetString();
            if (long.TryParse(str, out var seconds))
                return TimeSpan.FromSeconds(seconds);

            throw new JsonException($"Cannot convert \"{str}\" to TimeSpan.");
        }

        if (reader.TokenType == JsonTokenType.Number)
        {
            return TimeSpan.FromSeconds(reader.GetInt64());
        }

        throw new JsonException($"Unexpected token type {reader.TokenType} when reading TimeSpan.");
    }

    /// <summary>
    /// Writes a TimeSpan value as a number of seconds to JSON.
    /// </summary>
    /// <param name="writer">The JSON writer to write to.</param>
    /// <param name="value">The TimeSpan value to write.</param>
    /// <param name="options">The JSON serializer options.</param>
    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        => writer.WriteNumberValue((long)value.TotalSeconds);
}
