// Pattern 3: Child Resource CRUD
// Use when the resource is nested under a parent pre-created by the fixture.
// Example: Email under Person, WorkflowStep under Workflow.

using Crews.PlanningCenter.Api.{Product}.{Version};
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.{Product};

public class {Child}Tests({Product}Fixture fixture) : {Product}TestBase(fixture)
{
    [Fact]
    public async Task {Child}_FullCrudLifecycle()
    {
        string? {childId} = null;

        try
        {
            var {children} = Org.{Parents}.WithId(Fixture.{ParentId}).{Children};

            // -- Create --
            var createResult = await {children}.PostAsync(new {Child}
            {
                Name = $"IntTest-{UniqueId}"
            });
            Assert.NotNull(createResult.Data);
            {childId} = createResult.Data.Id;
            Assert.NotNull({childId});

            // -- Read --
            var readResult = await {children}.WithId({childId}).GetAsync();
            Assert.NotNull(readResult.Data);
            Assert.Equal({childId}, readResult.Data.Id);

            // -- Update --
            var updateResult = await {children}.WithId({childId}).PatchAsync(
                new {Child} { Name = $"IntTest-Updated-{UniqueId}" });
            Assert.NotNull(updateResult.Data);

            // -- Verify Update --
            var verifyResult = await {children}.WithId({childId}).GetAsync();
            Assert.Equal($"IntTest-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Name);

            // -- Delete --
            await {children}.WithId({childId}).DeleteAsync();
            {childId} = null;
        }
        finally
        {
            if ({childId} is not null)
            {
                try
                {
                    await Org.{Parents}.WithId(Fixture.{ParentId}).{Children}
                        .WithId({childId}).DeleteAsync();
                }
                catch { /* best effort */ }
            }
        }
    }
}
