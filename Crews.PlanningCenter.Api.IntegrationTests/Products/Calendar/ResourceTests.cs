using Crews.PlanningCenter.Api.Calendar.V2022_07_07;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class ResourceTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task Resource_FullCrudLifecycle()
	{
		string? resourceId = null;

		try
		{
			// -- Create --
			var createResult = await Org.Resources.PostAsync(
				new Resource { Name = $"IntTest-Resource-{UniqueId}" });
			Assert.NotNull(createResult.Data);
			resourceId = createResult.Data.Id;
			Assert.NotNull(resourceId);
			Assert.Equal($"IntTest-Resource-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await Org.Resources.WithId(resourceId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(resourceId, readResult.Data.Id);
			Assert.Equal($"IntTest-Resource-{UniqueId}", readResult.Data.Attributes?.Name);

			// -- Update --
			var updateResult = await Org.Resources.WithId(resourceId).PatchAsync(
				new Resource { Name = $"IntTest-Resource-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.Resources.WithId(resourceId).GetAsync();
			Assert.Equal($"IntTest-Resource-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Name);

			// -- Delete --
			await Org.Resources.WithId(resourceId).DeleteAsync();
			resourceId = null;
		}
		finally
		{
			if (resourceId is not null)
			{
				try
				{
					await Org.Resources.WithId(resourceId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
