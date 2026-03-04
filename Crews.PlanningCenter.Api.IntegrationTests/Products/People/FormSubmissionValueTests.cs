using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class FormSubmissionValueTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task FormSubmissionValue_GetAsync_ReturnsFormSubmissionValue()
	{
		var formId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "people/v2/forms");
		Skip.If(formId is null, "No form data available.");

		var submissionId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/forms/{formId}/form_submissions");
		Skip.If(submissionId is null, "No form submission data available.");

		var valueId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/forms/{formId}/form_submissions/{submissionId}/form_submission_values");
		Skip.If(valueId is null, "No form submission value data available.");

		var result = await Org.Forms.WithId(formId!).FormSubmissions.WithId(submissionId!)
			.FormSubmissionValues.WithId(valueId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
