using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class CarrierTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task Carrier_GetAsync_ReturnsCarrier()
	{
		var carrierId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "people/v2/carriers");
		Skip.If(carrierId is null, "No carrier data available.");

		var result = await Org.Carriers.WithId(carrierId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
