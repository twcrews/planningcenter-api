using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class SignupSheetTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task SignupSheet_GetCollectionAsync_ReturnsCollection()
	{
		var personId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "services/v2/people");
		Assert.NotNull(personId);

		var availableSignupId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"services/v2/people/{personId}/available_signups");

		if (availableSignupId is null)
		{
			// No available signups for this person — endpoint reachable but empty.
			var fallback = await HttpClient.GetAsync(
				$"services/v2/people/{personId}/available_signups?per_page=1");
			Assert.True(fallback.IsSuccessStatusCode);
			return;
		}

		var result = await Org.People.WithId(personId).AvailableSignups
			.WithId(availableSignupId).SignupSheets.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
