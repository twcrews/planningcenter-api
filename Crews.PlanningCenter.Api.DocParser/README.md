# Planning Center API Documentation Parser

This project is responsible for downloading and transpiling the Planning Center API documentation into a format that can be used as input in the code generator project.

This console application is designed to be executed manually as needed to synchronize this repository with the latest version of the Planning Center API documentation.

The files produced by this application will be automatically ingested at compile time by the code generator project as part of the [incremental source generation](https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md#syntaxvalueprovider) process.

## Configuration

The following custom configuration sections are supported in `appsettings.json`:

### General Settings

- `AppSettings:OutputDirectory`: the output directory for documentation JSON files. Defaults to `./output`.
- `AppSettings:PlanningCenterClient:BaseAddress`: the `HttpClient.BaseAddress` to use when fetching documentation.

### Documentation Builder Options

- `AppSettings:DocumentationBuilder:ConcurrentConnections`: the maximum number of concurrent requests allowed to the Planning Center API. **Please be respectful of Planning Center's servers when modifying this value.**

## Documentation Overrides

Overrides for correcting inaccuracies in the Planning Center API documentation are configured in a separate `overrides.json` file. This separates static data corrections from runtime application settings.

### Excluding Resources from Generation

- `ExcludedVertices`: a list of vertices (resources) to exclude from documentation generation. This value is an array of objects with the structure `{ Product: string?, Version: string?, Vertex: string, GenerateResource: bool, GenerateClients: bool }`.
  - If `Product` is null, the specified vertex will be excluded across all products, dependent on version.
  - If `Version` is null, the specified vertex will be excluded across all versions, dependent on product.
  - If both `Product` and `Version` are null, the specified vertex will be excluded across all products and versions.

### Name Overrides

- `NameOverrides`: a list of overrides for generated model and resource class names. This value is an array of objects with the structure `{ Product: string?, Version: string?, Vertex: string?, ModelName: string, ResourceName: string }`.

### Edge Type Overrides

- `EdgeTypeOverrides`: a list of overrides for outbound edge (associated resource) type names. This is necessary because in some instances, Planning Center's documentation does not accurately reflect the types of associated resources. This value is an array of objects with the structure `{ Product: string?, Version: string?, Vertex: string?, Edge: string, Type: string }`.
  - If `Product` is null, the specified edge override will be applied across all products, dependent on version and vertex.
  - If `Version` is null, the specified edge override will be applied across all versions, dependent on product and vertex.
  - If `Vertex` is null, the specified edge override will be applied across all vertices, dependent on product and version.
  - If `Product`, `Version`, and `Vertex` are all null, the specified edge override will be applied across all products, versions, and vertices.

### Collection Overrides

- `CollectionOverrides`: a list of overrides for whether an edge represents a collection or a singular resource. This value is an array of objects with the structure `{ Product: string?, Version: string?, Vertex: string?, Edge: string, IsCollection: bool }`.

### Attribute Type Overrides

- `AttributeTypeOverrides`: a list of overrides for resource attribute types. This value is an array of objects with the structure `{ Product: string?, Version: string?, Vertex: string?, Attribute: string, Type: string }`.
