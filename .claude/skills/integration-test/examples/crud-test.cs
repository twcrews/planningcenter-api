// Pattern 1: Full CRUD Lifecycle
// Replace {Product}, {Version}, {Resource}, {ResourceId} with actual values.

using Crews.PlanningCenter.Api.{Product}.{Version};
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.{Product};

public class {Resource}Tests({Product}Fixture fixture) : {Product}TestBase(fixture)
{
    [Fact]
    public async Task {Resource}_FullCrudLifecycle()
    {
        string? {resourceId} = null;

        try
        {
            // -- Create --
            var createResult = await Org.{Resources}.PostAsync(new {Resource}
            {
                Name = $"IntTest-{UniqueId}"
            });
            Assert.NotNull(createResult.Data);
            {resourceId} = createResult.Data.Id;
            Assert.NotNull({resourceId});
            Assert.Equal($"IntTest-{UniqueId}", createResult.Data.Attributes?.Name);

            // -- Read --
            var readResult = await Org.{Resources}.WithId({resourceId}).GetAsync();
            Assert.NotNull(readResult.Data);
            Assert.Equal({resourceId}, readResult.Data.Id);

            // -- Update --
            var updateResult = await Org.{Resources}.WithId({resourceId}).PatchAsync(
                new {Resource} { Name = $"IntTest-Updated-{UniqueId}" });
            Assert.NotNull(updateResult.Data);

            // -- Verify Update --
            var verifyResult = await Org.{Resources}.WithId({resourceId}).GetAsync();
            Assert.Equal($"IntTest-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Name);

            // -- Delete --
            await Org.{Resources}.WithId({resourceId}).DeleteAsync();
            {resourceId} = null;
        }
        finally
        {
            if ({resourceId} is not null)
            {
                try
                {
                    await Org.{Resources}.WithId({resourceId}).DeleteAsync();
                }
                catch { /* best effort */ }
            }
        }
    }
}
