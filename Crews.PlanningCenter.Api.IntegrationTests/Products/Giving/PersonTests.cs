using Crews.PlanningCenter.Api.Giving.V2019_10_18;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class PersonTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task Person_GetAsync_ReturnsPerson()
	{
		var result = await Org.People.WithId(Fixture.PersonId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}

	[Fact]
	public async Task Person_PatchAsync_UpdatesPerson()
	{
		var readResult = await Org.People.WithId(Fixture.PersonId).GetAsync();
		Assert.NotNull(readResult.Data);
		var originalName = readResult.Data.Attributes?.FirstName;

		var updateResult = await Org.People.WithId(Fixture.PersonId).PatchAsync(
			new Person { FirstName = originalName });
		Assert.NotNull(updateResult.Data);
		Assert.Equal(originalName, updateResult.Data.Attributes?.FirstName);
	}
}
