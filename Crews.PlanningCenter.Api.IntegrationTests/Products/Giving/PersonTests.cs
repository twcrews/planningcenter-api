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
		var originalNumber = readResult.Data.Attributes?.DonorNumber;

		var updateResult = await Org.People.WithId(Fixture.PersonId).PatchAsync(
			new Person { DonorNumber = readResult.Data.Attributes?.DonorNumber + 1 });
		Assert.NotNull(updateResult.Data);
		Assert.Equal(originalNumber + 1, updateResult.Data.Attributes?.DonorNumber);
	}
}
