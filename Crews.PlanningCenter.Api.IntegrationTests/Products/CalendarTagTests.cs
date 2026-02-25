using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.Calendar.V2022_07_07;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products;

[Trait("Product", "Calendar")]
public class CalendarTagTests(PlanningCenterFixture fixture) : IntegrationTestBase(fixture)
{
	[Fact]
	public async Task TagGroup_And_Tag_FullCrudLifecycle()
	{
		var calendarClient = new CalendarClient(HttpClient);
		var org = calendarClient.Latest;

		string? tagGroupId = null;
		string? tagId = null;

		try
		{
			// -- Create TagGroup --
			var tagGroupEndpoint = org.TagGroups;
			var tagGroupResult = await tagGroupEndpoint.PostAsync(
				new TagGroup { Name = $"IntTest-TG-{UniqueId}" });
			Assert.NotNull(tagGroupResult.Data);
			tagGroupId = tagGroupResult.Data.Id;
			Assert.NotNull(tagGroupId);

			// -- Create Tag under TagGroup --
			var createResult = await tagGroupEndpoint.WithId(tagGroupId).Tags.PostAsync(
				new Tag { Name = $"IntTest-Tag-{UniqueId}" });
			Assert.NotNull(createResult.Data);
			tagId = createResult.Data.Id;
			Assert.NotNull(tagId);
			Assert.Equal($"IntTest-Tag-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read Tag --
			var singleTagClient = org.TagGroups.WithId(tagGroupId).Tags.WithId(tagId);
			var readResult = await singleTagClient.GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(tagId, readResult.Data.Id);
			Assert.Equal($"IntTest-Tag-{UniqueId}", readResult.Data.Attributes?.Name);

			// -- Update Tag --
			var updateResult = await singleTagClient.PatchAsync(
				new Tag { Name = $"IntTest-Tag-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyClient = org.TagGroups.WithId(tagGroupId).Tags.WithId(tagId);
			var verifyResult = await verifyClient.GetAsync();
			Assert.Equal($"IntTest-Tag-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Name);

			// -- Delete Tag --
			var deleteTagClient = org.TagGroups.WithId(tagGroupId).Tags.WithId(tagId);
			await deleteTagClient.DeleteAsync();
			tagId = null;

			// -- Delete TagGroup --
			var deleteTagGroupClient = org.TagGroups.WithId(tagGroupId);
			await deleteTagGroupClient.DeleteAsync();
			tagGroupId = null;
		}
		finally
		{
			if (tagId is not null && tagGroupId is not null)
			{
				try
				{
					var cleanup = org.TagGroups.WithId(tagGroupId).Tags.WithId(tagId);
					await cleanup.DeleteAsync();
				}
				catch { /* best effort */ }
			}

			if (tagGroupId is not null)
			{
				try
				{
					var cleanup = org.TagGroups.WithId(tagGroupId);
					await cleanup.DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
