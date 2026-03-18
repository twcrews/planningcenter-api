using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class ServiceTypeTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task ServiceType_FullCrudLifecycle()
	{
		string? serviceTypeId = null;

		try
		{
			// -- Create --
			var createResult = await Org.ServiceTypes.PostAsync(new ServiceType
			{
				Name = $"IntTest-SvcType-{UniqueId}"
			});
			Assert.NotNull(createResult.Data);
			serviceTypeId = createResult.Data.Id;
			Assert.NotNull(serviceTypeId);
			Assert.Equal($"IntTest-SvcType-{UniqueId}",
				createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await Org.ServiceTypes.WithId(serviceTypeId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(serviceTypeId, readResult.Data.Id);
			Assert.Equal($"IntTest-SvcType-{UniqueId}",
				readResult.Data.Attributes?.Name);

			// -- Update --
			var updateResult = await Org.ServiceTypes.WithId(serviceTypeId).PatchAsync(new ServiceType
			{
				Name = $"IntTest-SvcType-Updated-{UniqueId}"
			});
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.ServiceTypes.WithId(serviceTypeId).GetAsync();
			Assert.Equal($"IntTest-SvcType-Updated-{UniqueId}",
				verifyResult.Data?.Attributes?.Name);

			// -- Delete --
			await Org.ServiceTypes.WithId(serviceTypeId).DeleteAsync();
			serviceTypeId = null;
		}
		finally
		{
			if (serviceTypeId is not null)
			{
				try
				{
					await Org.ServiceTypes.WithId(serviceTypeId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
