using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class JobStatusTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task JobStatus_GetAsync_ReturnsJobStatus()
	{
		// FIXME: I am unsure if a JobStatus resource can be persisted for use in testing.
		return;
	}
}
