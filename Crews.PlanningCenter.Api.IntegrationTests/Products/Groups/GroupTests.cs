using Crews.PlanningCenter.Api.Groups.V2023_07_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Groups;

public class GroupTests(GroupsFixture fixture) : GroupsTestBase(fixture)
{
	[Fact]
	public async Task Group_GetAsync_ReturnsGroup()
	{
		var result = await Org.Groups.WithId(Fixture.GroupId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}

	[Fact]
	public async Task Group_PatchAsync_UpdatesGroup()
	{
		var readResult = await Org.Groups.WithId(Fixture.GroupId!).GetAsync();
		Assert.NotNull(readResult.Data);

		var originalDescription = readResult.Data.Attributes?.Description;

		try
		{
			var updateResult = await Org.Groups.WithId(Fixture.GroupId!).PatchAsync(new Group
			{
				Name = $"IntTest-Name-{UniqueId}"
			});
			Assert.NotNull(updateResult.Data);

			var verifyResult = await Org.Groups.WithId(Fixture.GroupId!).GetAsync();
			Assert.Equal($"IntTest-Name-{UniqueId}", verifyResult.Data?.Attributes?.Name);
		}
		finally
		{
			try
			{
				await Org.Groups.WithId(Fixture.GroupId!).PatchAsync(new Group
				{
					Description = originalDescription
				});
			}
			catch { /* best effort */ }
		}
	}
}
