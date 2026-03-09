using System.Text.Json;
using System.Text.Json.Serialization;
using Crews.PlanningCenter.Api.Converters;

namespace Crews.PlanningCenter.Api.Tests.Converters;

public class StringFromNumberConverterTests
{
	private readonly JsonSerializerOptions _options = new()
	{
		Converters = { new StringFromNumberConverter() }
	};

	[Theory]
	[InlineData("42", "42")]
	[InlineData("0", "0")]
	[InlineData("-10", "-10")]
	[InlineData("9999999999", "9999999999")]
	public void Read_IntegerToken_ReturnsStringWithoutDecimal(string json, string expected)
	{
		// Act
		var result = JsonSerializer.Deserialize<string>(json, _options);

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void Read_FloatingPointToken_ReturnsStringRepresentation()
	{
		// Act
		var result = JsonSerializer.Deserialize<string>("3.14", _options);

		// Assert — value is the double representation of 3.14
		Assert.NotNull(result);
		Assert.Contains("3", result);
	}

	[Theory]
	[InlineData("\"hello\"", "hello")]
	[InlineData("\"123\"", "123")]
	[InlineData("\"\"", "")]
	public void Read_StringToken_ReturnsStringValue(string json, string expected)
	{
		// Act
		var result = JsonSerializer.Deserialize<string>(json, _options);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData("true")]
	[InlineData("false")]
	[InlineData("[1,2,3]")]
	public void Read_UnexpectedTokenType_ThrowsJsonException(string json)
	{
		// Act & Assert
		Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<string>(json, _options));
	}

	[Theory]
	[InlineData("hello")]
	[InlineData("")]
	[InlineData("42abc")]
	public void Write_String_WritesJsonString(string value)
	{
		// Act
		var result = JsonSerializer.Serialize(value, _options);

		// Assert
		Assert.Equal($"\"{value}\"", result);
	}

	[Fact]
	public void Read_AsProperty_WithNumberValue_DeserializesCorrectly()
	{
		// Arrange
		var json = """{"Value":99}""";

		// Act
		var result = JsonSerializer.Deserialize<StringWrapper>(json);

		// Assert
		Assert.Equal("99", result!.Value);
	}

	[Fact]
	public void Read_AsProperty_WithStringValue_DeserializesCorrectly()
	{
		// Arrange
		var json = """{"Value":"hello"}""";

		// Act
		var result = JsonSerializer.Deserialize<StringWrapper>(json);

		// Assert
		Assert.Equal("hello", result!.Value);
	}

	private class StringWrapper
	{
		[JsonConverter(typeof(StringFromNumberConverter))]
		public string Value { get; set; } = string.Empty;
	}
}
