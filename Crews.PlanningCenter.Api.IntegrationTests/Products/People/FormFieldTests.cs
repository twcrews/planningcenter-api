using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class FormFieldTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task FormField_GetAsync_ReturnsFormField()
	{
		var formId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "people/v2/forms");
		Skip.If(formId is null, "No form data available for form field tests.");

		var formFieldId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/forms/{formId}/form_fields");
		Skip.If(formFieldId is null, "No form field data available.");

		var result = await Org.Forms.WithId(formId!).FormFields.WithId(formFieldId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
