using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class ListTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task List_GetAsync_ReturnsList()
	{
		var result = await Org.Lists.WithId(Fixture.ListId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.Equal(Fixture.ListId, result.Data.Id);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}

	[Fact]
	public async Task List_PatchAsync_UpdatesList()
	{
		var getResult = await Org.Lists.WithId(Fixture.ListId!).GetAsync();
		var originalName = getResult.Data?.Attributes?.Name;

		var updateResult = await Org.Lists.WithId(Fixture.ListId!).PatchAsync(
			new List { Description = $"IntTest-Updated-{UniqueId}" });

		Assert.NotNull(updateResult.Data);
		Assert.True(updateResult.ResponseMessage?.IsSuccessStatusCode);

		// Restore original name if changed
		if (originalName is not null)
		{
			await Org.Lists.WithId(Fixture.ListId!).PatchAsync(
				new List { Name = originalName });
		}
	}
}
