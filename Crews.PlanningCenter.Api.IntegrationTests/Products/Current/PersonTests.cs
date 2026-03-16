using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Current;

public class PersonTests(CurrentFixture fixture) : CurrentTestBase(fixture)
{
	[Fact]
	public async Task Person_GetAsync_ReturnsPerson()
	{
		var result = await Org.People.WithId(Fixture.PersonId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.Equal(Fixture.PersonId, result.Data.Id);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
