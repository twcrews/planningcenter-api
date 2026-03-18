namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

/// <summary>
/// Fixture for Current product integration tests.
/// Pre-discovers shared resource IDs needed by child resource tests.
/// </summary>
public class CurrentFixture : PlanningCenterFixture
{
	/// <summary>ID of the current authenticated person.</summary>
	public string PersonId { get; private set; } = null!;

	public override async Task InitializeAsync()
	{
		await base.InitializeAsync();

		PersonId = (await CollectionReadHelper.GetFirstIdAsync(HttpClient, "current/v2/people"))!;
	}
}
