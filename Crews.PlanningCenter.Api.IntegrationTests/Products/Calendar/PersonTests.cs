using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class PersonTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task Person_GetAsync_ReturnsPerson()
	{
		var personId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "calendar/v2/people");
		var result = await Org.People.WithId(personId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
