using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class AvailableSignupTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task AvailableSignup_GetCollectionAsync_ReturnsCollection()
	{
		var personId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "services/v2/people");
		Assert.NotNull(personId);

		var result = await Org.People.WithId(personId).AvailableSignups.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
