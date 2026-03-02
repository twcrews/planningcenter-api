using Crews.PlanningCenter.Api.Calendar.V2022_07_07;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class TagTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task Tag_FullCrudLifecycle()
	{
		string? tagId = null;

		try
		{
			// -- Create --
			var createResult = await Org.TagGroups.WithId(Fixture.TagGroupId).Tags.PostAsync(
				new Tag { Name = $"IntTest-Tag-{UniqueId}" });
			Assert.NotNull(createResult.Data);
			tagId = createResult.Data.Id;
			Assert.NotNull(tagId);
			Assert.Equal($"IntTest-Tag-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await Org.Tags.WithId(tagId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(tagId, readResult.Data.Id);
			Assert.Equal($"IntTest-Tag-{UniqueId}", readResult.Data.Attributes?.Name);

			// -- Update --
			var updateResult = await Org.Tags.WithId(tagId).PatchAsync(
				new Tag { Name = $"IntTest-Tag-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.Tags.WithId(tagId).GetAsync();
			Assert.Equal($"IntTest-Tag-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Name);

			// -- Delete --
			await Org.Tags.WithId(tagId).DeleteAsync();
			tagId = null;
		}
		finally
		{
			if (tagId is not null)
			{
				try
				{
					await Org.Tags.WithId(tagId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
