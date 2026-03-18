namespace Crews.PlanningCenter.Api.Models.Tests;

public class ProductDefinitionTests
{
    [Fact]
    public void All_Contains_Expected_Products()
        => Assert.Equal(
            [
                ProductDefinition.Api,
                ProductDefinition.Calendar,
                ProductDefinition.CheckIns,
                ProductDefinition.Current,
                ProductDefinition.Giving,
                ProductDefinition.Groups,
                ProductDefinition.People,
                ProductDefinition.Publishing,
                ProductDefinition.Registrations,
                ProductDefinition.Services,
                ProductDefinition.Webhooks
            ],
            ProductDefinition.All);

    [Theory]
    [InlineData("api")]
    [InlineData("calendar")]
    [InlineData("check-ins")]
    [InlineData("current")]
    [InlineData("giving")]
    [InlineData("groups")]
    [InlineData("people")]
    [InlineData("publishing")]
    [InlineData("registrations")]
    [InlineData("services")]
    [InlineData("webhooks")]
    public void All_Contains_Product_With_Name(string name)
        => Assert.Contains(ProductDefinition.All, p => (string)p == name);

    [Theory]
    [InlineData("api")]
    [InlineData("calendar")]
    [InlineData("check-ins")]
    [InlineData("current")]
    [InlineData("giving")]
    [InlineData("groups")]
    [InlineData("people")]
    [InlineData("publishing")]
    [InlineData("registrations")]
    [InlineData("services")]
    [InlineData("webhooks")]
    public void ToString_Returns_Name(string name)
        => Assert.Contains(ProductDefinition.All, p => p.ToString() == name);

    [Theory]
    [InlineData("api")]
    [InlineData("calendar")]
    [InlineData("check-ins")]
    [InlineData("current")]
    [InlineData("giving")]
    [InlineData("groups")]
    [InlineData("people")]
    [InlineData("publishing")]
    [InlineData("registrations")]
    [InlineData("services")]
    [InlineData("webhooks")]
    public void ImplicitStringConversion_Returns_Name(string name)
        => Assert.Contains(ProductDefinition.All, p => (string)p == name);
}
