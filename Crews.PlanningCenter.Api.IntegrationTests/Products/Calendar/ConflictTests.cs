using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class ConflictTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task Conflict_GetAsync_ReturnsConflict()
	{
		var conflictId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "calendar/v2/conflicts");
		var result = await Org.Conflicts.WithId(conflictId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
