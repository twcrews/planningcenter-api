// Pattern 4: Relationship-Required Resource
// Use when the POST body requires a Relationships block (e.g., WorkflowCard requires Person).

using Crews.PlanningCenter.Api.{Product}.{Version};
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.{Product};

public class {Resource}Tests({Product}Fixture fixture) : {Product}TestBase(fixture)
{
    [Fact]
    public async Task {Resource}_FullCrudLifecycle()
    {
        string? {resourceId} = null;

        try
        {
            var {resources} = Org.{Parents}.WithId(Fixture.{ParentId}).{Resources};

            // -- Create --
            var createResult = await {resources}.PostAsync(
                new JsonApiDocument<{Resource}Resource>
                {
                    Data = new()
                    {
                        Attributes = new {Resource} { /* set required attributes */ },
                        Relationships = new()
                        {
                            {RelatedResource} = new()
                            {
                                Data = new() { Type = "{RelatedType}", Id = Fixture.{RelatedId} }
                            }
                        }
                    }
                });
            Assert.NotNull(createResult.Data);
            {resourceId} = createResult.Data.Id;
            Assert.NotNull({resourceId});

            // -- Read --
            var readResult = await {resources}.WithId({resourceId}).GetAsync();
            Assert.NotNull(readResult.Data);
            Assert.Equal({resourceId}, readResult.Data.Id);

            // -- Update --
            var updateResult = await {resources}.WithId({resourceId}).PatchAsync(
                new {Resource} { /* set updatable attributes */ });
            Assert.NotNull(updateResult.Data);

            // -- Delete --
            await {resources}.WithId({resourceId}).DeleteAsync();
            {resourceId} = null;
        }
        finally
        {
            if ({resourceId} is not null)
            {
                try
                {
                    await Org.{Parents}.WithId(Fixture.{ParentId}).{Resources}
                        .WithId({resourceId}).DeleteAsync();
                }
                catch { /* best effort */ }
            }
        }
    }
}
