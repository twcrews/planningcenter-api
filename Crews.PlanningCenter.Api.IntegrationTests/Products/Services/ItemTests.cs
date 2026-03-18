using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class ItemTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task Item_FullCrudLifecycle()
	{
		string? itemId = null;

		try
		{
			// -- Create --
			var createResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).Items.PostAsync(new Item
				{
					Title = $"IntTest-Item-{UniqueId}"
				});
			Assert.NotNull(createResult.Data);
			itemId = createResult.Data.Id;
			Assert.NotNull(itemId);
			Assert.Equal($"IntTest-Item-{UniqueId}", createResult.Data.Attributes?.Title);

			// -- Read --
			var readResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).Items.WithId(itemId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(itemId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).Items.WithId(itemId).PatchAsync(new Item
				{
					Title = $"IntTest-Item-Updated-{UniqueId}"
				});
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).Items.WithId(itemId).GetAsync();
			Assert.Equal($"IntTest-Item-Updated-{UniqueId}",
				verifyResult.Data?.Attributes?.Title);

			// -- Delete --
			await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).Items.WithId(itemId).DeleteAsync();
			itemId = null;
		}
		finally
		{
			if (itemId is not null)
			{
				try
				{
					await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
						.Plans.WithId(Fixture.PlanId).Items.WithId(itemId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
