using Crews.PlanningCenter.Api.DocParser.Models;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.DocParser.Services;

public interface IPlanningCenterClient
{
    Task<GraphDocument> GetGraphAsync(ProductDefinition product);
    Task<GraphVersionDocument> GetGraphVersionAsync(ProductDefinition product, string versionId);
    Task<VertexDocument> GetVertexAsync(ProductDefinition product, string versionId, string vertexId);
}
