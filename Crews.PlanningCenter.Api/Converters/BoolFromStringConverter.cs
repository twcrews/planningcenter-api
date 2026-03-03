using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crews.PlanningCenter.Api.Converters;

/// <summary>
/// A custom JSON converter that handles boolean values represented as strings in the JSON payload.
/// </summary>
public class BoolFromStringConverter : JsonConverter<bool>
{
    /// <summary>
    /// Reads a boolean value from a JSON string. Accepts "true", "false", "1", and "0" (case-insensitive) as valid inputs.
    /// </summary>
    /// <param name="reader">The JSON reader to read from.</param>
    /// <param name="typeToConvert">The type to convert to.</param>
    /// <param name="options">The JSON serializer options.</param>
    /// <returns>The converted boolean value.</returns>
    /// <exception cref="JsonException">Thrown when the value cannot be converted to a boolean.</exception>
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value?.ToLowerInvariant() switch
        {
            "true" or "1" => true,
            "false" or "0" => false,
            _ => throw new JsonException($"Unable to convert '{value}' to boolean.")
        };
    }

    /// <summary>
    /// Writes a boolean value as a JSON boolean. This converter does not support writing boolean values as strings.
    /// </summary>
    /// <param name="writer">The JSON writer to write to.</param>
    /// <param name="value">The boolean value to write.</param>
    /// <param name="options">The JSON serializer options.</param>
    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) 
        => writer.WriteBooleanValue(value);
}