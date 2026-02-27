namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

/// <summary>
/// Fixture for Publishing product integration tests.
/// Pre-discovers shared parent resource IDs needed by child resource tests.
/// </summary>
public class PublishingFixture : PlanningCenterFixture
{
	/// <summary>ID of an existing Series for Episode tests, or null if none exist.</summary>
	public string? SeriesId { get; private set; }

	/// <summary>ID of an existing Channel for child resource tests, or null if none exist.</summary>
	public string? ChannelId { get; private set; }

	public override async Task InitializeAsync()
	{
		await base.InitializeAsync();

		// Series and Channel cannot be created at the org level; discover existing ones.
		SeriesId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "publishing/v2/series");
		ChannelId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "publishing/v2/channels");
	}
}
