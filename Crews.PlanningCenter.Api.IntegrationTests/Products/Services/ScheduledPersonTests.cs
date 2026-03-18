using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class ScheduledPersonTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task ScheduledPerson_GetCollectionAsync_ReturnsCollection()
	{
		var personId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "services/v2/people");
		Assert.NotNull(personId);

		var availableSignupId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"services/v2/people/{personId}/available_signups");

		if (availableSignupId is null)
		{
			var fallback = await HttpClient.GetAsync(
				$"services/v2/people/{personId}/available_signups?per_page=1");
			Assert.True(fallback.IsSuccessStatusCode);
			return;
		}

		var signupSheetId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient,
			$"services/v2/people/{personId}/available_signups/{availableSignupId}/signup_sheets");

		if (signupSheetId is null)
		{
			var fallback = await HttpClient.GetAsync(
				$"services/v2/people/{personId}/available_signups/{availableSignupId}/signup_sheets?per_page=1");
			Assert.True(fallback.IsSuccessStatusCode);
			return;
		}

		var result = await Org.People.WithId(personId).AvailableSignups
			.WithId(availableSignupId).SignupSheets.WithId(signupSheetId).ScheduledPeople.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
