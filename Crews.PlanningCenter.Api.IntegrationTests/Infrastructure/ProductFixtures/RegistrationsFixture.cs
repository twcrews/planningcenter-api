namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

/// <summary>
/// Fixture for Registrations product integration tests.
/// Registrations is a read-only product; existing resources are discovered at initialization.
/// </summary>
public class RegistrationsFixture : PlanningCenterFixture
{
	/// <summary>ID of an existing Campus for tests.</summary>
	public string CampusId { get; private set; } = null!;

	/// <summary>ID of an existing Category for tests.</summary>
	public string CategoryId { get; private set; } = null!;

	/// <summary>ID of an existing Signup for tests.</summary>
	public string SignupId { get; private set; } = null!;

	/// <summary>ID of an existing Attendee for tests.</summary>
	public string AttendeeId { get; private set; } = null!;

	/// <summary>ID of an existing Registration for tests.</summary>
	public string RegistrationId { get; private set; } = null!;

	/// <summary>ID of an existing SelectionType for tests.</summary>
	public string SelectionTypeId { get; private set; } = null!;

	/// <summary>ID of an existing SignupTime for tests.</summary>
	public string SignupTimeId { get; private set; } = null!;

	public override async Task InitializeAsync()
	{
		await base.InitializeAsync();

		// Top-level collections
		CampusId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, "registrations/v2/campuses"))!;
		CategoryId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, "registrations/v2/categories"))!;
		SignupId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, "registrations/v2/signups"))!;

		// Child collections requiring a parent Signup ID
		if (SignupId is not null)
		{
			AttendeeId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"registrations/v2/signups/{SignupId}/attendees"))!;
			RegistrationId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"registrations/v2/signups/{SignupId}/registrations"))!;
			SelectionTypeId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"registrations/v2/signups/{SignupId}/selection_types"))!;
			SignupTimeId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"registrations/v2/signups/{SignupId}/signup_times"))!;
		}
	}
}
