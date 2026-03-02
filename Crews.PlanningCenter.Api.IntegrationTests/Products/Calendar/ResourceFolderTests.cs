using Crews.PlanningCenter.Api.Calendar.V2022_07_07;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class ResourceFolderTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task ResourceFolder_FullCrudLifecycle()
	{
		string? resourceFolderId = null;

		try
		{
			// -- Create --
			var createResult = await Org.ResourceFolders.PostAsync(
				new ResourceFolder { Name = $"IntTest-RF-{UniqueId}", Kind = "Resource" });
			Assert.NotNull(createResult.Data);
			resourceFolderId = createResult.Data.Id;
			Assert.NotNull(resourceFolderId);
			Assert.Equal($"IntTest-RF-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await Org.ResourceFolders.WithId(resourceFolderId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(resourceFolderId, readResult.Data.Id);
			Assert.Equal($"IntTest-RF-{UniqueId}", readResult.Data.Attributes?.Name);

			// -- Update --
			var updateResult = await Org.ResourceFolders.WithId(resourceFolderId).PatchAsync(
				new ResourceFolder { Name = $"IntTest-RF-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.ResourceFolders.WithId(resourceFolderId).GetAsync();
			Assert.Equal($"IntTest-RF-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Name);

			// -- Delete --
			await Org.ResourceFolders.WithId(resourceFolderId).DeleteAsync();
			resourceFolderId = null;
		}
		finally
		{
			if (resourceFolderId is not null)
			{
				try
				{
					await Org.ResourceFolders.WithId(resourceFolderId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
