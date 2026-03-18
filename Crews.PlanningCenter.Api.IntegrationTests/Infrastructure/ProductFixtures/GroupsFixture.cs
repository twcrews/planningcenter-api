namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

/// <summary>
/// Fixture for Groups product integration tests.
/// Pre-fetches shared parent resource IDs needed by child resource tests.
/// </summary>
public class GroupsFixture : PlanningCenterFixture
{
	/// <summary>ID of an existing Group, or null if none exist.</summary>
	public string? GroupId { get; private set; }

	/// <summary>ID of an existing Person, or null if none exist.</summary>
	public string? PersonId { get; private set; }

	/// <summary>ID of an existing Event, or null if none exist.</summary>
	public string? EventId { get; private set; }

	/// <summary>ID of an existing TagGroup, or null if none exist.</summary>
	public string? TagGroupId { get; private set; }

	public override async Task InitializeAsync()
	{
		await base.InitializeAsync();

		GroupId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "groups/v2/groups");
		PersonId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "groups/v2/people");
		EventId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "groups/v2/events");
		TagGroupId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "groups/v2/tag_groups");
	}
}
