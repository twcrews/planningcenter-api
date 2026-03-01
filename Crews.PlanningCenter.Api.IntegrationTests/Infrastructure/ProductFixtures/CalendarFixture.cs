using Crews.PlanningCenter.Api.Calendar.V2022_07_07;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

/// <summary>
/// Fixture for Calendar product integration tests.
/// Pre-creates shared parent resources needed by child resource tests.
/// </summary>
public class CalendarFixture : PlanningCenterFixture
{
	string _fixtureId = null!;

	/// <summary>ID of a pre-created TagGroup for Tag tests.</summary>
	public string TagGroupId { get; private set; } = null!;

	/// <summary>ID of an existing Event for child resource tests, or null if none exist.</summary>
	public string EventId { get; private set; } = null!;

	public string EventConnectionResourceId { get; private set; } = null!;

	/// <summary>ID of an existing EventInstance for child resource tests, or null if none exist.</summary>
	public string EventInstanceId { get; private set; } = null!;

	/// <summary>ID of an existing EventResourceRequest for child resource tests, or null if none exist.</summary>
	public string EventResourceRequestId { get; private set; } = null!;

	/// <summary>ID of an existing RoomSetup for ContainingResource tests, or null if none exist.</summary>
	public string RoomSetupId { get; private set; } = null!;

	/// <summary>ID of a pre-created Resource for ResourceQuestion tests.</summary>
	public string ResourceId { get; private set; } = null!;

	public override async Task InitializeAsync()
	{
		await base.InitializeAsync();
		_fixtureId = Guid.NewGuid().ToString("N")[..8];

		var org = new CalendarClient(HttpClient).Latest;

		var tagGroupResult = await org.TagGroups.PostAsync(
			new TagGroup { Name = $"Fixture-TG-{_fixtureId}" });
		TagGroupId = tagGroupResult.Data!.Id!;

		var resourceResult = await org.Resources.PostAsync(
			new Resource { Name = $"Fixture-Resource-{_fixtureId}" });
		ResourceId = resourceResult.Data!.Id!;

		var eventId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "calendar/v2/events");
		EventId = eventId!;

		var eventConnectionResourceId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"groups/v2/groups");
		EventConnectionResourceId = eventConnectionResourceId!;

		var eventInstanceId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "calendar/v2/event_instances");
		EventInstanceId = eventInstanceId!;

		var eventResourceRequestId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "calendar/v2/event_resource_requests");
		EventResourceRequestId = eventResourceRequestId!;

		var roomSetupId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "calendar/v2/room_setups");
		RoomSetupId = roomSetupId!;
	}

	public override async Task DisposeAsync()
	{
		var org = new CalendarClient(HttpClient).Latest;

		try { await org.Resources.WithId(ResourceId).DeleteAsync(); } catch { }
		try { await org.TagGroups.WithId(TagGroupId).DeleteAsync(); } catch { }

		await base.DisposeAsync();
	}
}
