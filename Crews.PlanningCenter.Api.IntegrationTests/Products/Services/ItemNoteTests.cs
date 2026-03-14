using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class ItemNoteTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task ItemNote_GetCollectionAsync_ReturnsCollection()
	{
		string? itemId = null;

		try
		{
			var itemResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).Items.PostAsync(new Item
				{
					Title = $"IntTest-Item-{UniqueId}",
					ItemType = "song"
				});
			Assert.NotNull(itemResult.Data);
			itemId = itemResult.Data.Id;
			Assert.NotNull(itemId);

			var result = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).Items.WithId(itemId).ItemNotes.GetAsync();
			Assert.NotNull(result);
			Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
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
