using Crews.PlanningCenter.Api.Services.V2018_11_01;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

/// <summary>
/// Fixture for Services product integration tests.
/// Pre-creates shared parent resources needed by child resource tests.
/// </summary>
public class ServicesFixture : PlanningCenterFixture
{
	string _fixtureId = null!;

	/// <summary>ID of a pre-created ServiceType for Plan and other tests.</summary>
	public string ServiceTypeId { get; private set; } = null!;

	/// <summary>ID of a pre-created Plan (under ServiceType) for Item, PlanTime, and other tests.</summary>
	public string PlanId { get; private set; } = null!;

	/// <summary>ID of a pre-created Song for Arrangement tests.</summary>
	public string SongId { get; private set; } = null!;

	public override async Task InitializeAsync()
	{
		await base.InitializeAsync();
		_fixtureId = Guid.NewGuid().ToString("N")[..8];

		var org = new ServicesClient(HttpClient).Latest;

		var serviceTypeResult = await org.ServiceTypes.PostAsync(
			new ServiceType { Name = $"Fixture-SvcType-{_fixtureId}" });
		ServiceTypeId = serviceTypeResult.Data!.Id!;

		var planResult = await org.ServiceTypes.WithId(ServiceTypeId).Plans.PostAsync(
			new Plan { Title = $"Fixture-Plan-{_fixtureId}" });
		PlanId = planResult.Data!.Id!;

		var songResult = await org.Songs.PostAsync(
			new Song { Title = $"Fixture-Song-{_fixtureId}" });
		SongId = songResult.Data!.Id!;
	}

	public override async Task DisposeAsync()
	{
		var org = new ServicesClient(HttpClient).Latest;

		try { await org.Songs.WithId(SongId).DeleteAsync(); } catch { }
		try { await org.ServiceTypes.WithId(ServiceTypeId).Plans.WithId(PlanId).DeleteAsync(); } catch { }
		try { await org.ServiceTypes.WithId(ServiceTypeId).DeleteAsync(); } catch { }

		await base.DisposeAsync();
	}
}
