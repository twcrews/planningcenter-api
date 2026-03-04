using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class AddressTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task Address_FullCrudLifecycle()
	{
		string? addressId = null;

		try
		{
			var personAddresses = Org.People.WithId(Fixture.PersonId).Addresses;

			// -- Create --
			var createResult = await personAddresses.PostAsync(new Address
			{
				StreetLine1 = $"123 IntTest St {UniqueId}",
				City = "Oklahoma City",
				State = "OK",
				Zip = "73013",
				Location = "Home"
			});
			Assert.NotNull(createResult.Data);
			addressId = createResult.Data.Id;
			Assert.NotNull(addressId);
			Assert.Equal($"123 IntTest St {UniqueId}", createResult.Data.Attributes?.StreetLine1);

			// -- Read --
			var readResult = await personAddresses.WithId(addressId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(addressId, readResult.Data.Id);
			Assert.Equal("Home", readResult.Data.Attributes?.Location);

			// -- Update --
			var updateResult = await personAddresses.WithId(addressId).PatchAsync(
				new Address { Location = "Work" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await personAddresses.WithId(addressId).GetAsync();
			Assert.Equal("Work", verifyResult.Data?.Attributes?.Location);

			// -- Delete --
			await personAddresses.WithId(addressId).DeleteAsync();
			addressId = null;
		}
		finally
		{
			if (addressId is not null)
			{
				try
				{
					await Org.People.WithId(Fixture.PersonId).Addresses.WithId(addressId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
