using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class SeriesTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task Series_GetAsync_ReturnsSeries()
	{
		var result = await Org.Series.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
