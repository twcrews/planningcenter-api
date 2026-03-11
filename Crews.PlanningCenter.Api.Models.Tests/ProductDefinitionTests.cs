namespace Crews.PlanningCenter.Api.Models.Tests;

public class ProductDefinitionTests
{
    [Fact]
    public void All_Contains_Eight_Products()
        => Assert.Equal(8, ProductDefinition.All.Count());

    [Fact]
    public void All_Contains_Expected_Products()
        => Assert.Equal(
            [
                ProductDefinition.Calendar,
                ProductDefinition.CheckIns,
                ProductDefinition.Giving,
                ProductDefinition.Groups,
                ProductDefinition.People,
                ProductDefinition.Publishing,
                ProductDefinition.Registrations,
                ProductDefinition.Services
            ],
            ProductDefinition.All);

    [Theory]
    [InlineData("calendar")]
    [InlineData("check-ins")]
    [InlineData("giving")]
    [InlineData("groups")]
    [InlineData("people")]
    [InlineData("publishing")]
    [InlineData("registrations")]
    [InlineData("services")]
    public void All_Contains_Product_With_Name(string name)
        => Assert.Contains(ProductDefinition.All, p => (string)p == name);

    [Theory]
    [InlineData("calendar")]
    [InlineData("check-ins")]
    [InlineData("giving")]
    [InlineData("groups")]
    [InlineData("people")]
    [InlineData("publishing")]
    [InlineData("registrations")]
    [InlineData("services")]
    public void ToString_Returns_Name(string name)
        => Assert.Contains(ProductDefinition.All, p => p.ToString() == name);

    [Theory]
    [InlineData("calendar")]
    [InlineData("check-ins")]
    [InlineData("giving")]
    [InlineData("groups")]
    [InlineData("people")]
    [InlineData("publishing")]
    [InlineData("registrations")]
    [InlineData("services")]
    public void ImplicitStringConversion_Returns_Name(string name)
        => Assert.Contains(ProductDefinition.All, p => (string)p == name);
}
