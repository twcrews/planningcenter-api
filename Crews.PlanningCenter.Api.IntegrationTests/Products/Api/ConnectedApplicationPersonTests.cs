using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Api;

public class ConnectedApplicationPersonTests(ApiFixture fixture) : ApiTestBase(fixture)
{
	[Fact]
	public async Task ConnectedApplicationPerson_GetAsync_ReturnsConnectedApplicationPerson()
	{
		Assert.NotNull(Fixture.ConnectedApplicationId);
		Assert.NotNull(Fixture.ConnectedApplicationPersonId);

		var result = await Org.ConnectedApplications
			.WithId(Fixture.ConnectedApplicationId)
			.People
			.WithId(Fixture.ConnectedApplicationPersonId)
			.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.Equal(Fixture.ConnectedApplicationPersonId, result.Data.Id);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
