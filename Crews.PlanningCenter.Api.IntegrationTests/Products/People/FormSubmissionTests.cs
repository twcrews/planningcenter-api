using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class FormSubmissionTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task FormSubmission_GetAsync_ReturnsFormSubmission()
	{
		var formId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "people/v2/forms");
		Skip.If(formId is null, "No form data available for form submission tests.");

		var submissionId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/forms/{formId}/form_submissions");
		Skip.If(submissionId is null, "No form submission data available.");

		var result = await Org.Forms.WithId(formId!).FormSubmissions.WithId(submissionId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
