using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class EventResourceRequestTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task EventResourceRequest_GetAsync_ReturnsEventResourceRequest()
	{
		var result = await Org.EventResourceRequests.WithId(Fixture.EventResourceRequestId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
