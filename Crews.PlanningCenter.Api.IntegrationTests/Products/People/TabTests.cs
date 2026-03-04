using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class TabTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task Tab_FullCrudLifecycle()
	{
		string? tabId = null;

		try
		{
			// -- Create --
			var createResult = await Org.Tabs.PostAsync(
				new Tab { Name = $"IntTest-{UniqueId}" });
			Assert.NotNull(createResult.Data);
			tabId = createResult.Data.Id;
			Assert.NotNull(tabId);
			Assert.Equal($"IntTest-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await Org.Tabs.WithId(tabId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(tabId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.Tabs.WithId(tabId).PatchAsync(
				new Tab { Name = $"IntTest-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.Tabs.WithId(tabId).GetAsync();
			Assert.Equal($"IntTest-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Name);

			// -- Delete --
			await Org.Tabs.WithId(tabId).DeleteAsync();
			tabId = null;
		}
		finally
		{
			if (tabId is not null)
			{
				try
				{
					await Org.Tabs.WithId(tabId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
