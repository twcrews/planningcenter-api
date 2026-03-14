using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class MediaTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task Media_GetCollectionAsync_ReturnsCollection()
	{
		var result = await Org.Media.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
