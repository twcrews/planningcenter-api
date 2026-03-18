using Crews.PlanningCenter.Api.Calendar.V2022_07_07;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class TagGroupTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task TagGroup_And_Tag_FullCrudLifecycle()
	{
		string? tagGroupId = null;
		string? tagId = null;

		try
		{
			// -- Create TagGroup --
			var tagGroupResult = await Org.TagGroups.PostAsync(
				new TagGroup { Name = $"IntTest-TG-{UniqueId}" });
			Assert.NotNull(tagGroupResult.Data);
			tagGroupId = tagGroupResult.Data.Id;
			Assert.NotNull(tagGroupId);

			// -- Create Tag under TagGroup --
			var createResult = await Org.TagGroups.WithId(tagGroupId).Tags.PostAsync(
				new Tag { Name = $"IntTest-Tag-{UniqueId}" });
			Assert.NotNull(createResult.Data);
			tagId = createResult.Data.Id;
			Assert.NotNull(tagId);
			Assert.Equal($"IntTest-Tag-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read Tag --
			var readResult = await Org.TagGroups.WithId(tagGroupId).Tags.WithId(tagId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(tagId, readResult.Data.Id);
			Assert.Equal($"IntTest-Tag-{UniqueId}", readResult.Data.Attributes?.Name);

			// -- Update Tag --
			var updateResult = await Org.TagGroups.WithId(tagGroupId).Tags.WithId(tagId).PatchAsync(
				new Tag { Name = $"IntTest-Tag-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.TagGroups.WithId(tagGroupId).Tags.WithId(tagId).GetAsync();
			Assert.Equal($"IntTest-Tag-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Name);

			// -- Delete Tag --
			await Org.TagGroups.WithId(tagGroupId).Tags.WithId(tagId).DeleteAsync();
			tagId = null;

			// -- Delete TagGroup --
			await Org.TagGroups.WithId(tagGroupId).DeleteAsync();
			tagGroupId = null;
		}
		finally
		{
			if (tagId is not null && tagGroupId is not null)
			{
				try
				{
					await Org.TagGroups.WithId(tagGroupId).Tags.WithId(tagId).DeleteAsync();
				}
				catch { /* best effort */ }
			}

			if (tagGroupId is not null)
			{
				try
				{
					await Org.TagGroups.WithId(tagGroupId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
