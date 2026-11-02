using System.Text.Json.Nodes;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;

/// <summary>
/// Offline tests for the rules that decide what may be deleted from the live account.
/// </summary>
public class TestDataTests
{
	private static readonly DateTimeOffset Now = new(2026, 11, 2, 0, 0, 0, TimeSpan.Zero);

	private static string IdAt(DateTimeOffset time) => $"{time.ToUnixTimeSeconds():x8}abcd";

	[Theory]
	[InlineData("Fixture-Tab-fd67a3c6")]
	[InlineData("Fixture Person-fd67a3c6")]
	[InlineData("IntTest-Song-20c93d65")]
	[InlineData("IntTest")]
	[InlineData("IntTest-Updated Person-20c93d65")]
	[InlineData("TempMember IntTest-20c93d65")]
	[InlineData("inttest-label-70709d74-upd")]
	public void IsTagged_WithTestData_ReturnsTrue(string value) => Assert.True(TestData.IsTagged(value));

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData("Thomas Crews")]
	[InlineData("Test Workflow")]
	[InlineData("Fixture")]
	[InlineData("Fixture Repair Fund")]
	[InlineData("IntTestimony")]
	[InlineData("My IntTest-Song")]
	[InlineData("TempMember Smith")]
	public void IsTagged_WithRealData_ReturnsFalse(string? value) => Assert.False(TestData.IsTagged(value));

	[Fact]
	public void NewId_EmbedsCreationTime()
	{
		var before = DateTimeOffset.UtcNow.AddSeconds(-1);

		var createdAt = TestData.GetCreatedAt($"IntTest-Song-{TestData.NewId()}-upd");

		Assert.NotNull(createdAt);
		Assert.InRange(createdAt.Value, before, DateTimeOffset.UtcNow.AddSeconds(1));
	}

	[Theory]
	[InlineData("IntTest-Song-20c93d65")]
	[InlineData("IntTest")]
	public void GetCreatedAt_WithoutTimestampedId_ReturnsNull(string value) => Assert.Null(TestData.GetCreatedAt(value));

	[Fact]
	public void IsStaleTestData_WithOldEmbeddedTime_ReturnsTrue()
	{
		var record = Record(("name", $"IntTest-{IdAt(Now.AddHours(-2))}"));

		Assert.True(TestDataSweeper.IsStaleTestData(record, Now));
	}

	[Fact]
	public void IsStaleTestData_WithRecentEmbeddedTime_ReturnsFalse()
	{
		var record = Record(("name", $"IntTest-{IdAt(Now.AddMinutes(-5))}"));

		Assert.False(TestDataSweeper.IsStaleTestData(record, Now));
	}

	[Fact]
	public void IsStaleTestData_WithLegacyIdAndRecentCreatedAt_ReturnsFalse()
	{
		var record = Record(("name", "Fixture-Tab-fd67a3c6"), ("created_at", Now.AddMinutes(-5).ToString("O")));

		Assert.False(TestDataSweeper.IsStaleTestData(record, Now));
	}

	[Fact]
	public void IsStaleTestData_WithLegacyIdAndNoCreatedAt_ReturnsTrue()
	{
		var record = Record(("name", "Fixture-Tab-fd67a3c6"));

		Assert.True(TestDataSweeper.IsStaleTestData(record, Now));
	}

	[Fact]
	public void IsStaleTestData_WithUntaggedRecord_ReturnsFalse()
	{
		var record = Record(("name", "Thomas Crews"), ("created_at", Now.AddYears(-1).ToString("O")));

		Assert.False(TestDataSweeper.IsStaleTestData(record, Now));
	}

	private static JsonNode Record(params (string Key, string Value)[] attributes)
	{
		JsonObject values = [];
		foreach (var (key, value) in attributes)
			values[key] = value;

		return new JsonObject { ["attributes"] = values };
	}
}
