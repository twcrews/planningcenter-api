using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class SkippedAttachmentTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task SkippedAttachment_GetCollectionAsync_ReturnsCollection()
	{
		var response = await HttpClient.GetAsync(
			$"services/v2/service_types/{Fixture.ServiceTypeId}/plans/{Fixture.PlanId}" +
			$"/attachments?per_page=1");
		Assert.True(response.IsSuccessStatusCode);

		// Verify skipped_attachments endpoint is accessible on the plan
		var skippedResponse = await HttpClient.GetAsync(
			$"services/v2/service_types/{Fixture.ServiceTypeId}/plans/{Fixture.PlanId}" +
			$"/skipped_attachments?per_page=1");
		Assert.True(skippedResponse.IsSuccessStatusCode);
	}
}
