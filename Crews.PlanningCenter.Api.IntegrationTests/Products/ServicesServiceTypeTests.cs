using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.Services.V2018_11_01;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products;

[Trait("Product", "Services")]
public class ServicesServiceTypeTests(PlanningCenterFixture fixture) : IntegrationTestBase(fixture)
{
	[Fact]
	public async Task ServiceType_FullCrudLifecycle()
	{
		var servicesClient = new ServicesClient(HttpClient);
		var org = servicesClient.Latest;

		string? serviceTypeId = null;

		try
		{
			// -- Create --
			var endpoint = org.ServiceTypes;
			var createResult = await endpoint.PostAsync(new ServiceType
			{
				Name = $"IntTest-SvcType-{UniqueId}"
			});
			Assert.NotNull(createResult.Data);
			serviceTypeId = createResult.Data.Id;
			Assert.NotNull(serviceTypeId);
			Assert.Equal($"IntTest-SvcType-{UniqueId}",
				createResult.Data.Attributes?.Name);

			// -- Read --
			var singleClient = org.ServiceTypes.WithId(serviceTypeId);
			var readResult = await singleClient.GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(serviceTypeId, readResult.Data.Id);
			Assert.Equal($"IntTest-SvcType-{UniqueId}",
				readResult.Data.Attributes?.Name);

			// -- Update --
			var updateClient = org.ServiceTypes.WithId(serviceTypeId);
			var updateResult = await updateClient.PatchAsync(new ServiceType
			{
				Name = $"IntTest-SvcType-Updated-{UniqueId}"
			});
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyClient = org.ServiceTypes.WithId(serviceTypeId);
			var verifyResult = await verifyClient.GetAsync();
			Assert.Equal($"IntTest-SvcType-Updated-{UniqueId}",
				verifyResult.Data?.Attributes?.Name);

			// -- Delete --
			var deleteClient = org.ServiceTypes.WithId(serviceTypeId);
			await deleteClient.DeleteAsync();
			serviceTypeId = null;
		}
		finally
		{
			if (serviceTypeId is not null)
			{
				try
				{
					var cleanup = org.ServiceTypes.WithId(serviceTypeId);
					await cleanup.DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
