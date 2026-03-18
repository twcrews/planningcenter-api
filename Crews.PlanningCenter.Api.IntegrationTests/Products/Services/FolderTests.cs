using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class FolderTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task Folder_FullCrudLifecycle()
	{
		string? folderId = null;

		try
		{
			// -- Create --
			var createResult = await Org.Folders.PostAsync(new Folder
			{
				Name = $"IntTest-Folder-{UniqueId}"
			});
			Assert.NotNull(createResult.Data);
			folderId = createResult.Data.Id;
			Assert.NotNull(folderId);
			Assert.Equal($"IntTest-Folder-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await Org.Folders.WithId(folderId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(folderId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.Folders.WithId(folderId).PatchAsync(
				new Folder { Name = $"IntTest-Folder-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.Folders.WithId(folderId).GetAsync();
			Assert.Equal($"IntTest-Folder-Updated-{UniqueId}",
				verifyResult.Data?.Attributes?.Name);

			// -- Delete --
			await Org.Folders.WithId(folderId).DeleteAsync();
			folderId = null;
		}
		finally
		{
			if (folderId is not null)
			{
				try { await Org.Folders.WithId(folderId).DeleteAsync(); }
				catch { /* best effort */ }
			}
		}
	}
}
