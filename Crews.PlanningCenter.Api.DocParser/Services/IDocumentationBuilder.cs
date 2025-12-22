using Crews.PlanningCenter.Api.DocParser.Models;
using Crews.PlanningCenter.Api.DocParser.Models.Outgoing;

namespace Crews.PlanningCenter.Api.DocParser.Services;

interface IDocumentationBuilder
{
    Task<IEnumerable<Product>> BuildAllProductsAsync();
    Task<Product> BuildProductAsync(ProductDefinition product);
}
