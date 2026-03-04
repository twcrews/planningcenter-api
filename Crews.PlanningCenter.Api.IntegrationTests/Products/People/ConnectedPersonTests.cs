using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class ConnectedPersonTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task ConnectedPerson_GetAsync_ReturnsConnectedPerson()
	{
		var connectedPersonId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/people/{Fixture.PersonId}/connected_people");

		var result = await Org.People.WithId(Fixture.PersonId).ConnectedPeople
			.WithId(connectedPersonId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
