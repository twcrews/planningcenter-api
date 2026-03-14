using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class BlockoutScheduleConflictTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task BlockoutScheduleConflict_GetCollectionAsync_ReturnsCollection()
	{
		// FIXME: I'm not able to find this resource or its endpoints.
		// I think it's possible it represents the `SignupSheetMetadata.Conflicts` attribute.
	}
}
