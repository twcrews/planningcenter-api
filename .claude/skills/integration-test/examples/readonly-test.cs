// Pattern 2: Read-Only (Get)
// Use when the resource only supports GET.

using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.{Product};

public class {Resource}Tests({Product}Fixture fixture) : {Product}TestBase(fixture)
{
    [Fact]
    public async Task {Resource}_GetAsync_Returns{Resource}()
    {
        var {resourceId} = await CollectionReadHelper.GetFirstIdAsync(
            HttpClient, "{product}/v2/{resources}");

        var result = await Org.{Resources}.WithId({resourceId}!).GetAsync();

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
    }
}
