using Crews.PlanningCenter.Api.DocParser;
using Crews.PlanningCenter.Api.DocParser.Models;
using Crews.PlanningCenter.Api.DocParser.Models.Incoming;
using Attribute = Crews.PlanningCenter.Api.DocParser.Models.Incoming.Attribute;

PlanningCenterClient client = new(new());

foreach (ProductDefinition product in ProductDefinition.All)
{
  GraphDocument graphDocument = await client.GetGraphAsync(product);
  Console.WriteLine();
  Console.WriteLine("===================");
  Console.WriteLine(product.ToString().ToUpper());
  Console.WriteLine("===================");

  foreach (VersionResource version in graphDocument.Data.Relationships.Versions.Data)
  {
    Console.WriteLine();
    Console.WriteLine($"- {version.Id}");

    GraphVersionDocument versionDocument = await client.GetGraphVersionAsync(product, version.Id!);
    foreach (VertexResource vertex in versionDocument.Data.Relationships.Vertices.Data)
    {
      Console.WriteLine($"  - {vertex.Attributes?.Name ?? "******* UNDEFINED VERTEX *******"}");

      VertexDocument vertexDocument = await client.GetVertexAsync(product, version.Id!, vertex.Id!);
      foreach (Attribute attribute in vertexDocument.Data.Relationships.Attributes.Data.Select(resource => resource.Attributes))
      {
        Console.WriteLine($"    - {attribute.Name ?? "******* UNDEFINED ATTRIBUTE *******"}");
      }
    }
  }
}