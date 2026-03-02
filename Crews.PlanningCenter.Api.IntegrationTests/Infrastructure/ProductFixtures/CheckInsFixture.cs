using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

/// <summary>
/// Fixture for CheckIns product integration tests.
/// CheckIns is a read-only product; existing resources are discovered at initialization.
/// </summary>
public class CheckInsFixture : PlanningCenterFixture
{
	/// <summary>ID of an existing AttendanceType for tests.</summary>
	public string AttendanceTypeId { get; private set; } = null!;

	/// <summary>ID of an existing CheckIn for tests.</summary>
	public string CheckInId { get; private set; } = null!;

	/// <summary>ID of an existing CheckInTime for tests.</summary>
	public string CheckInTimeId { get; private set; } = null!;

	/// <summary>ID of an existing Event for tests.</summary>
	public string EventId { get; private set; } = null!;

	/// <summary>ID of an existing EventLabel for tests.</summary>
	public string EventLabelId { get; private set; } = null!;

	/// <summary>ID of an existing EventPeriod for tests.</summary>
	public string EventPeriodId { get; private set; } = null!;

	/// <summary>ID of an existing EventTime for tests.</summary>
	public string EventTimeId { get; private set; } = null!;

	/// <summary>ID of an existing Headcount for tests.</summary>
	public string HeadcountId { get; private set; } = null!;

	/// <summary>ID of an existing IntegrationLink for tests.</summary>
	public string IntegrationLinkId { get; private set; } = null!;

	/// <summary>ID of an existing Label for tests.</summary>
	public string LabelId { get; private set; } = null!;

	/// <summary>ID of an existing Location for tests.</summary>
	public string LocationId { get; private set; } = null!;

	/// <summary>ID of an existing LocationEventPeriod for tests.</summary>
	public string LocationEventPeriodId { get; private set; } = null!;

	/// <summary>ID of an existing LocationEventTime for tests.</summary>
	public string LocationEventTimeId { get; private set; } = null!;

	/// <summary>ID of an existing LocationLabel for tests.</summary>
	public string LocationLabelId { get; private set; } = null!;

	/// <summary>ID of an existing Option for tests.</summary>
	public string OptionId { get; private set; } = null!;

	/// <summary>ID of an existing Pass for tests.</summary>
	public string PassId { get; private set; } = null!;

	/// <summary>ID of an existing Person for tests.</summary>
	public string PersonId { get; private set; } = null!;

	/// <summary>ID of an existing PersonEvent for tests.</summary>
	public string PersonEventId { get; private set; } = null!;

	/// <summary>ID of an existing Station for tests.</summary>
	public string StationId { get; private set; } = null!;

	/// <summary>ID of an existing Theme for tests.</summary>
	public string ThemeId { get; private set; } = null!;

	public override async Task InitializeAsync()
	{
		await base.InitializeAsync();

		// Top-level collections
		CheckInId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, "check-ins/v2/check_ins"))!;
		EventId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, "check-ins/v2/events"))!;
		EventTimeId = (await CollectionReadHelper.GetLastIdAsync(HttpClient, "check-ins/v2/event_times"))!;
		HeadcountId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, "check-ins/v2/headcounts"))!;
		IntegrationLinkId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, "check-ins/v2/integration_links"))!;
		LabelId = (await CollectionReadHelper.GetLastIdAsync(HttpClient, "check-ins/v2/labels"))!;
		OptionId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, "check-ins/v2/options"))!;
		PassId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, "check-ins/v2/passes"))!;
		PersonId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, "check-ins/v2/people"))!;
		StationId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, "check-ins/v2/stations"))!;
		ThemeId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, "check-ins/v2/themes"))!;

		// Child collections requiring a parent ID
		if (CheckInId is not null)
			CheckInTimeId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"check-ins/v2/check_ins/{CheckInId}/check_in_times"))!;

		if (EventId is not null)
		{
			AttendanceTypeId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"check-ins/v2/events/{EventId}/attendance_types"))!;
			EventLabelId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"check-ins/v2/events/{EventId}/event_labels"))!;
			EventPeriodId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"check-ins/v2/events/{EventId}/event_periods"))!;
			LocationId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"check-ins/v2/events/{EventId}/locations"))!;
			PersonEventId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"check-ins/v2/events/{EventId}/person_events"))!;

			if (EventPeriodId is not null)
				LocationEventPeriodId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"check-ins/v2/events/{EventId}/event_periods/{EventPeriodId}/location_event_periods"))!;
		}

		if (EventTimeId is not null)
			LocationEventTimeId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"check-ins/v2/event_times/{EventTimeId}/location_event_times"))!;

		if (LabelId is not null)
			LocationLabelId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"check-ins/v2/labels/{LabelId}/location_labels"))!;
	}
}
