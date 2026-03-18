using System.Text.Json;
using System.Text.Json.Serialization;
using Crews.PlanningCenter.Api.Converters;

namespace Crews.PlanningCenter.Api.Tests.Converters;

public class BoolFromStringConverterTests
{
	private readonly JsonSerializerOptions _options = new()
	{
		Converters = { new BoolFromStringConverter() }
	};

	[Theory]
	[InlineData("\"true\"", true)]
	[InlineData("\"false\"", false)]
	[InlineData("\"1\"", true)]
	[InlineData("\"0\"", false)]
	[InlineData("\"TRUE\"", true)]
	[InlineData("\"FALSE\"", false)]
	[InlineData("\"True\"", true)]
	[InlineData("\"False\"", false)]
	[InlineData("\"yes\"", true)]
	[InlineData("\"no\"", false)]
	[InlineData("\"Yes\"", true)]
	[InlineData("\"No\"", false)]
	[InlineData("\"YES\"", true)]
	[InlineData("\"NO\"", false)]
	public void Read_ValidInput_ReturnsExpectedBool(string json, bool expected)
	{
		// Act
		var result = JsonSerializer.Deserialize<bool>(json, _options);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData("\"\"")]
	[InlineData("\"maybe\"")]
	[InlineData("null")]
	[InlineData("2")]
	public void Read_InvalidInput_ThrowsJsonException(string json)
	{
		// Act & Assert
		Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<bool>(json, _options));
	}

	[Theory]
	[InlineData(true, "true")]
	[InlineData(false, "false")]
	public void Write_Bool_WritesJsonBoolean(bool value, string expectedJson)
	{
		// Act
		var result = JsonSerializer.Serialize(value, _options);

		// Assert
		Assert.Equal(expectedJson, result);
	}

	[Fact]
	public void Read_AsProperty_DeserializesCorrectly()
	{
		// Arrange
		var json = """{"Value":"true"}""";

		// Act
		var result = JsonSerializer.Deserialize<BoolWrapper>(json);

		// Assert
		Assert.True(result!.Value);
	}

	private class BoolWrapper
	{
		[JsonConverter(typeof(BoolFromStringConverter))]
		public bool Value { get; set; }
	}
}
