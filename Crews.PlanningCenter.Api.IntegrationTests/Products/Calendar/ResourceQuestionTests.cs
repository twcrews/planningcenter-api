using Crews.PlanningCenter.Api.Calendar.V2022_07_07;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class ResourceQuestionTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task ResourceQuestion_FullCrudLifecycle()
	{
		string? questionId = null;

		try
		{
			// -- Create --
			var createResult = await Org.Resources.WithId(Fixture.ResourceId).ResourceQuestions.PostAsync(
				new ResourceQuestion { Kind = "text", Question = $"IntTest-Q-{UniqueId}?" });
			Assert.NotNull(createResult.Data);
			questionId = createResult.Data.Id;
			Assert.NotNull(questionId);
			Assert.Equal($"IntTest-Q-{UniqueId}?", createResult.Data.Attributes?.Question);

			// -- Read --
			var readResult = await Org.ResourceQuestions.WithId(questionId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(questionId, readResult.Data.Id);
			Assert.Equal($"IntTest-Q-{UniqueId}?", readResult.Data.Attributes?.Question);

			// -- Update --
			var updateResult = await Org.ResourceQuestions.WithId(questionId).PatchAsync(
				new ResourceQuestion { Question = $"IntTest-Q-Updated-{UniqueId}?" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.ResourceQuestions.WithId(questionId).GetAsync();
			Assert.Equal($"IntTest-Q-Updated-{UniqueId}?", verifyResult.Data?.Attributes?.Question);

			// -- Delete --
			await Org.ResourceQuestions.WithId(questionId).DeleteAsync();
			questionId = null;
		}
		finally
		{
			if (questionId is not null)
			{
				try
				{
					await Org.ResourceQuestions.WithId(questionId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
