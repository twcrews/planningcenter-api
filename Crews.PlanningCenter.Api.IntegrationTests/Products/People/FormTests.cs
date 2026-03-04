using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class FormTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task Form_GetAsync_ReturnsForm()
	{
		var formId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "people/v2/forms");
		Skip.If(formId is null, "No form data available.");

		var result = await Org.Forms.WithId(formId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
