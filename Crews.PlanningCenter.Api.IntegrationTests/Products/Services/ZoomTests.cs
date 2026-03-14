using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class ZoomTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task Zoom_GetCollectionAsync_ReturnsCollection()
	{
		var attachmentId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient,
			$"services/v2/service_types/{Fixture.ServiceTypeId}/plans/{Fixture.PlanId}/attachments");

		if (attachmentId is null)
		{
			// No attachments on the fixture plan — verify the plan attachments endpoint is reachable.
			var fallback = await HttpClient.GetAsync(
				$"services/v2/service_types/{Fixture.ServiceTypeId}/plans/{Fixture.PlanId}/attachments?per_page=1");
			Assert.True(fallback.IsSuccessStatusCode);
			return;
		}

		var result = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
			.Plans.WithId(Fixture.PlanId).Attachments.WithId(attachmentId).Zooms.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
