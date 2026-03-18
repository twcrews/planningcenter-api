namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

/// <summary>
/// Fixture for Api product integration tests.
/// Pre-discovers shared resource IDs needed by child resource tests.
/// </summary>
public class ApiFixture : PlanningCenterFixture
{
	/// <summary>ID of an existing ConnectedApplication for tests, or null if none exist.</summary>
	public string? ConnectedApplicationId { get; private set; }

	/// <summary>ID of an existing ConnectedApplicationPerson for tests, or null if none exist.</summary>
	public string? ConnectedApplicationPersonId { get; private set; }

	/// <summary>ID of an existing OauthApplication for tests, or null if none exist.</summary>
	public string? OauthApplicationId { get; private set; }

	/// <summary>ID of an existing PersonalAccessToken for tests, or null if none exist.</summary>
	public string? PersonalAccessTokenId { get; private set; }

	public override async Task InitializeAsync()
	{
		await base.InitializeAsync();

		ConnectedApplicationId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "api/v2/connected_applications");

		if (ConnectedApplicationId is not null)
		{
			ConnectedApplicationPersonId = await CollectionReadHelper.GetFirstIdAsync(
				HttpClient, $"api/v2/connected_applications/{ConnectedApplicationId}/people");
		}

		OauthApplicationId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "api/v2/oauth_applications");

		PersonalAccessTokenId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "api/v2/personal_access_tokens");
	}
}
