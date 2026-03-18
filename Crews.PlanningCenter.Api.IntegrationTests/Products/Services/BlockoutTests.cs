using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class BlockoutTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task Blockout_FullCrudLifecycle()
	{
		var personId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "services/v2/people");
		Assert.NotNull(personId);

		string? blockoutId = null;

		try
		{
			var startsAt = DateTime.UtcNow.AddDays(30);
			var endsAt = startsAt.AddHours(2);

			// -- Create --
			var createResult = await Org.People.WithId(personId).Blockouts.PostAsync(new Blockout
			{
				StartsAt = startsAt,
				EndsAt = endsAt,
				RepeatFrequency = "no_repeat"
			});
			Assert.NotNull(createResult.Data);
			blockoutId = createResult.Data.Id;
			Assert.NotNull(blockoutId);

			// -- Read --
			var readResult = await Org.People.WithId(personId).Blockouts
				.WithId(blockoutId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(blockoutId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.People.WithId(personId).Blockouts
				.WithId(blockoutId).PatchAsync(new Blockout
				{
					Reason = "Some reason"
				});
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.People.WithId(personId).Blockouts
				.WithId(blockoutId).GetAsync();
			Assert.Equal($"Some reason",
				verifyResult.Data?.Attributes?.Reason);

			// -- Delete --
			await Org.People.WithId(personId).Blockouts.WithId(blockoutId).DeleteAsync();
			blockoutId = null;
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
