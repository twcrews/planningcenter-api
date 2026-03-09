using System.Text.Json;
using System.Text.Json.Serialization;
using Crews.PlanningCenter.Api.Converters;

namespace Crews.PlanningCenter.Api.Tests.Converters;

public class TimeSpanFromSecondsConverterTests
{
	private readonly JsonSerializerOptions _options = new()
	{
		Converters = { new TimeSpanFromSecondsConverter() }
	};

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(3600)]
	[InlineData(86400)]
	[InlineData(-60)]
	public void Read_NumberToken_ReturnsTimeSpanFromSeconds(long seconds)
	{
		// Act
		var result = JsonSerializer.Deserialize<TimeSpan>(seconds.ToString(), _options);

		// Assert
		Assert.Equal(TimeSpan.FromSeconds(seconds), result);
	}

	[Theory]
	[InlineData("\"0\"", 0)]
	[InlineData("\"3600\"", 3600)]
	[InlineData("\"86400\"", 86400)]
	[InlineData("\"-60\"", -60)]
	public void Read_StringTokenWithNumericContent_ReturnsTimeSpanFromSeconds(string json, long seconds)
	{
		// Act
		var result = JsonSerializer.Deserialize<TimeSpan>(json, _options);

		// Assert
		Assert.Equal(TimeSpan.FromSeconds(seconds), result);
	}

	[Theory]
	[InlineData("\"abc\"")]
	[InlineData("\"3.14\"")]
	[InlineData("\"\"")]
	public void Read_StringTokenWithNonNumericContent_ThrowsJsonException(string json)
	{
		// Act & Assert
		Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<TimeSpan>(json, _options));
	}

	[Fact]
	public void Read_UnexpectedTokenType_ThrowsJsonException()
	{
		// Arrange — boolean is neither String nor Number
		var json = "true";

		// Act & Assert
		Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<TimeSpan>(json, _options));
	}

	[Theory]
	[InlineData(0)]
	[InlineData(3600)]
	[InlineData(86400)]
	[InlineData(-60)]
	public void Write_TimeSpan_WritesSecondCountAsNumber(long seconds)
	{
		// Arrange
		var timeSpan = TimeSpan.FromSeconds(seconds);

		// Act
		var result = JsonSerializer.Serialize(timeSpan, _options);

		// Assert
		Assert.Equal(seconds.ToString(), result);
	}

	[Fact]
	public void Read_AsProperty_WithNumberValue_DeserializesCorrectly()
	{
		// Arrange
		var json = """{"Value":7200}""";

		// Act
		var result = JsonSerializer.Deserialize<TimeSpanWrapper>(json);

		// Assert
		Assert.Equal(TimeSpan.FromHours(2), result!.Value);
	}

	[Fact]
	public void Read_AsProperty_WithStringValue_DeserializesCorrectly()
	{
		// Arrange
		var json = """{"Value":"7200"}""";

		// Act
		var result = JsonSerializer.Deserialize<TimeSpanWrapper>(json);

		// Assert
		Assert.Equal(TimeSpan.FromHours(2), result!.Value);
	}

	private class TimeSpanWrapper
	{
		[JsonConverter(typeof(TimeSpanFromSecondsConverter))]
		public TimeSpan Value { get; set; }
	}
}
