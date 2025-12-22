using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.DocParser.Services;

interface IDocumentationBuilder
{
    Task<IEnumerable<Product>> BuildAllProductsAsync();
    Task<Product> BuildProductAsync(ProductDefinition product);
}
