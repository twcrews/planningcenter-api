using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class BirthdayPeopleTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task BirthdayPeople_GetAsync_ReturnsResponse()
	{
		var result = await Org.BirthdayPeople.GetAsync();

		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
