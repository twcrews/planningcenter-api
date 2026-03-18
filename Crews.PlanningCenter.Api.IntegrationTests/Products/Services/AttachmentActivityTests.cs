using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class AttachmentActivityTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task AttachmentActivity_GetCollectionAsync_ReturnsCollection()
	{
		var attachmentId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient,
			$"services/v2/service_types/{Fixture.ServiceTypeId}/plans/{Fixture.PlanId}/attachments");

		if (attachmentId is null)
		{
			// No attachments on the fixture plan — endpoint is still reachable.
			var fallback = await HttpClient.GetAsync(
				$"services/v2/service_types/{Fixture.ServiceTypeId}/plans/{Fixture.PlanId}/attachments?per_page=1");
			Assert.True(fallback.IsSuccessStatusCode);
			return;
		}

		var response = await HttpClient.GetAsync(
			$"services/v2/service_types/{Fixture.ServiceTypeId}/plans/{Fixture.PlanId}" +
			$"/attachments/{attachmentId}/attachment_activities?per_page=1");
		Assert.True(response.IsSuccessStatusCode);
	}
}
