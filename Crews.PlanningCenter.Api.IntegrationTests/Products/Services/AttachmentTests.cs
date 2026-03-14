using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class AttachmentTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task Attachment_GetCollectionAsync_ReturnsCollection()
	{
		var result = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
			.Plans.WithId(Fixture.PlanId).Attachments.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
