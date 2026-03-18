using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class PersonTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task Person_GetCollectionAsync_ReturnsCollection()
	{
		var result = await Org.People.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}

	[Fact]
	public async Task Person_GetAsync_ReturnsPerson()
	{
		var personId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "services/v2/people");
		Assert.NotNull(personId);

		var result = await Org.People.WithId(personId).GetAsync();
		Assert.NotNull(result.Data);
		Assert.Equal(personId, result.Data.Id);
	}
}
