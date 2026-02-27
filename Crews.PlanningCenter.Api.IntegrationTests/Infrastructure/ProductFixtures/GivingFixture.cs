using Crews.PlanningCenter.Api.Giving.V2019_10_18;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

/// <summary>
/// Fixture for Giving product integration tests.
/// Pre-creates shared parent resources needed by child resource tests.
/// </summary>
public class GivingFixture : PlanningCenterFixture
{
	string _fixtureId = null!;

	/// <summary>ID of a pre-created Fund for Designation and other tests.</summary>
	public string FundId { get; private set; } = null!;

	/// <summary>ID of a pre-created Batch for Donation tests.</summary>
	public string BatchId { get; private set; } = null!;

	public override async Task InitializeAsync()
	{
		await base.InitializeAsync();
		_fixtureId = Guid.NewGuid().ToString("N")[..8];

		var org = new GivingClient(HttpClient).Latest;

		var fundResult = await org.Funds.PostAsync(
			new Fund { Name = $"Fixture-Fund-{_fixtureId}" });
		FundId = fundResult.Data!.Id!;

		var batchResult = await org.Batches.PostAsync(
			new Batch { Description = $"Fixture-Batch-{_fixtureId}" });
		BatchId = batchResult.Data!.Id!;
	}

	public override async Task DisposeAsync()
	{
		var org = new GivingClient(HttpClient).Latest;

		try { await org.Batches.WithId(BatchId).DeleteAsync(); } catch { }
		try { await org.Funds.WithId(FundId).DeleteAsync(); } catch { }

		await base.DisposeAsync();
	}
}
