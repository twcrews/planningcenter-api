using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Registrations;

public class CategoryTests(RegistrationsFixture fixture) : RegistrationsTestBase(fixture)
{
	[Fact]
	public async Task Category_GetAsync_ReturnsCategory()
	{
		var result = await Org.Categories.WithId(Fixture.CategoryId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
