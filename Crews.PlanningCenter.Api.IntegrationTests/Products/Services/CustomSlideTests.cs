using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class CustomSlideTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task CustomSlide_FullCrudLifecycle()
	{
		string? itemId = null;
		string? customSlideId = null;

		try
		{
			// -- Create parent Item --
			var itemResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).Items.PostAsync(new Item
				{
					Title = $"IntTest-Item-{UniqueId}",
					ItemType = "song"
				});
			Assert.NotNull(itemResult.Data);
			itemId = itemResult.Data.Id;
			Assert.NotNull(itemId);

			// -- Create CustomSlide --
			var createResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).Items.WithId(itemId).CustomSlides
				.PostAsync(new CustomSlide
				{
					Label = $"IntTest-Slide-{UniqueId}",
					Body = "Test slide body",
					Enabled = true
				});
			Assert.NotNull(createResult.Data);
			customSlideId = createResult.Data.Id;
			Assert.NotNull(customSlideId);

			// -- Read --
			var readResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).Items.WithId(itemId).CustomSlides
				.WithId(customSlideId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(customSlideId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).Items.WithId(itemId).CustomSlides
				.WithId(customSlideId).PatchAsync(new CustomSlide
				{
					Label = $"IntTest-Slide-Updated-{UniqueId}"
				});
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).Items.WithId(itemId).CustomSlides
				.WithId(customSlideId).GetAsync();
			Assert.Equal($"IntTest-Slide-Updated-{UniqueId}",
				verifyResult.Data?.Attributes?.Label);

			// -- Delete CustomSlide --
			await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).Items.WithId(itemId).CustomSlides
				.WithId(customSlideId).DeleteAsync();
			customSlideId = null;
		}
		finally
		{
			if (customSlideId is not null)
			{
				try
				{
					await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
						.Plans.WithId(Fixture.PlanId).Items.WithId(itemId!).CustomSlides
						.WithId(customSlideId).DeleteAsync();
				}
				catch { /* best effort */ }
			}

			if (itemId is not null)
			{
				try
				{
					await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
						.Plans.WithId(Fixture.PlanId).Items.WithId(itemId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
