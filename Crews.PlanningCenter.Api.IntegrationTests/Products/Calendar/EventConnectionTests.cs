using Crews.PlanningCenter.Api.Calendar.V2022_07_07;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class EventConnectionTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task EventConnection_FullCrudLifecycle()
	{
		string? connectionId = null;

		try
		{
			// -- Create --
			var createResult = await Org.Events.WithId(Fixture.EventId).EventConnections.PostAsync(
				new EventConnection
				{
					ConnectedToId = Convert.ToInt32(Fixture.EventConnectionResourceId),
					ConnectedToName = $"IntTest-Connection-{UniqueId}",
					ConnectedToType = "group",
					ProductName = "groups"
				});
			Assert.NotNull(createResult.Data);
			connectionId = createResult.Data.Id;
			Assert.NotNull(connectionId);
			Assert.Equal($"IntTest-Connection-{UniqueId}", createResult.Data.Attributes?.ConnectedToName);

			// -- Read --
			var readResult = await Org.Events.WithId(Fixture.EventId).EventConnections.WithId(connectionId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(connectionId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.Events.WithId(Fixture.EventId).EventConnections.WithId(connectionId).PatchAsync(
				new EventConnection { ConnectedToName = $"IntTest-Connection-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.Events.WithId(Fixture.EventId).EventConnections.WithId(connectionId).GetAsync();
			Assert.Equal($"IntTest-Connection-Updated-{UniqueId}", verifyResult.Data?.Attributes?.ConnectedToName);

			// -- Delete --
			await Org.Events.WithId(Fixture.EventId).EventConnections.WithId(connectionId).DeleteAsync();
			connectionId = null;
		}
		finally
		{
			if (connectionId is not null && Fixture.EventId is not null)
			{
				try
				{
					await Org.Events.WithId(Fixture.EventId).EventConnections.WithId(connectionId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
