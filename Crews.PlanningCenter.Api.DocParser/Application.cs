using Crews.PlanningCenter.Api.DocParser.Models;
using Crews.PlanningCenter.Api.DocParser.Services;
using Crews.PlanningCenter.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Crews.PlanningCenter.Api.DocParser;

class Application(ILogger<Application> logger, IConfiguration configuration, IPlanningCenterClient client)
{
    public async Task RunAsync()
    {
        List<string> printedTypes = [];

        foreach (ProductDefinition product in ProductDefinition.All)
        {
            GraphDocument graphDoc = await client.GetGraphAsync(product);

            foreach (string? version in graphDoc.Data.Relationships.Versions.Data.Select(v => v.Id))
            {
                GraphVersionDocument versionDoc = await client.GetGraphVersionAsync(product, version!);

                foreach (string? vertex in versionDoc.Data.Relationships.Vertices.Data.Select(v => v.Id))
                {
                    VertexDocument vertexDoc = await client.GetVertexAsync(product, version!, vertex!);

                    foreach (string type in vertexDoc.Data.Relationships!.Attributes.Data.Select(a => a.Attributes.TypeAnnotation.Name))
                    {
                        if (!printedTypes.Contains(type))
                        {
                            printedTypes.Add(type);
                            logger.LogInformation("Discovered type '{Type}' at {Product}.{Version}.{Vertex}", type, product, version, vertex);
                        }
                    }
                }
            }
        }
    }
}
