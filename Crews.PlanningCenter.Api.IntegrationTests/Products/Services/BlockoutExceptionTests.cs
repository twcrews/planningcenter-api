using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class BlockoutExceptionTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task BlockoutException_GetCollectionAsync_ReturnsCollection()
	{
		var personId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "services/v2/people");
		Assert.NotNull(personId);

		string? blockoutId = null;

		try
		{
			var startsAt = DateTime.UtcNow.AddDays(30);
			var createResult = await Org.People.WithId(personId).Blockouts.PostAsync(new Blockout
			{
				StartsAt = startsAt,
				EndsAt = startsAt.AddHours(2),
				RepeatFrequency = "no_repeat"
			});
			blockoutId = createResult.Data!.Id!;

			var result = await Org.People.WithId(personId).Blockouts
				.WithId(blockoutId).BlockoutExceptions.GetAsync();
			Assert.NotNull(result);
			Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
		}
		finally
		{
			if (blockoutId is not null)
			{
				try
				{
					await Org.People.WithId(personId!).Blockouts.WithId(blockoutId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
