using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Groups;

public class PersonTests(GroupsFixture fixture) : GroupsTestBase(fixture)
{
	[Fact]
	public async Task Person_GetAsync_ReturnsPerson()
	{
		var result = await Org.People.WithId(Fixture.PersonId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
