using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class EmailTemplateRenderedResponseTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task EmailTemplateRenderedResponse_GetCollectionAsync_ReturnsCollection()
	{
		var emailTemplateId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, "services/v2/email_templates");
		Assert.NotNull(emailTemplateId);

		await Org.EmailTemplates.WithId(emailTemplateId).GetAsync();
	}
}
