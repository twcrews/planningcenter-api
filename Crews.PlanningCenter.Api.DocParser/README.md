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

## Documentation Transforms

Transforms for correcting inaccuracies in the Planning Center API documentation are configured in a separate `transforms.json` file. This separates static data corrections from runtime application settings.

Every entry can be limited with these optional properties:

- `Product`: the product name as it appears in the API path (e.g., `check-ins`). If null, the entry applies across all products.
- `Version`: the API version (e.g., `2025-11-10`). If null, the entry applies across all versions.

Entries are matched in file order, and only the first matching override is applied to each item. List narrowly scoped entries before broader ones.

See [DocumentationTransforms.cs](Configuration/DocumentationTransforms.cs) for every property that each entry supports. Any property left out of an override keeps the value from the documentation.

### Resource Overrides

- `ResourceOverrides`: a list of overrides for resource properties, such as class names, permissions, and whether to generate the resource or its clients. `Resource` is required.

### Resource Child Overrides

- `ResourceChildOverrides`: a list of overrides for a resource's child endpoints, such as whether a child is a collection or which URL slug it uses. `Child` is required. If `Resource` is null, the override applies across all resources.

### Attribute Overrides

- `AttributeOverrides`: a list of overrides for resource attributes, such as the CLR type or JSON converter. `Attribute` is required. If `Resource` is null, the override applies across all resources.

### Relationship Overrides

- `RelationshipOverrides`: a list of overrides for resource relationships. This is necessary because in some instances, Planning Center's documentation does not accurately reflect the types of related resources. `Relationship` is required. If `Resource` is null, the override applies across all resources.

If a relationship's type is not defined in the same product and version, the generated code will not compile. To fix this, set `ResourceClrType` to `Crews.Web.JsonApiClient.JsonApiResourceIdentifier`.

### Additional Resource Children

- `AdditionalResourceChildren`: a list of child endpoints to add to a resource when they are missing from the documentation. `Resource` and `Child` are required.
