using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class ReportTemplateTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task ReportTemplate_GetCollectionAsync_ReturnsCollection()
	{
		var result = await Org.ReportTemplates.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
