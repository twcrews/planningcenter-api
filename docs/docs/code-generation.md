# Code Generation

How the client code is automatically generated from Planning Center API documentation.

## Overview

The Planning Center API client library uses a two-phase approach to generate strongly-typed client code:

1. **DocParser** - Transpiles API documentation into JSON definitions
2. **Source Generator** - Generates C# code from JSON definitions at compile time

## Running the DocParser

Update API definitions when Planning Center releases new versions:

```bash
cd Crews.PlanningCenter.Api.DocParser
dotnet run
```

This will:
- Download API metadata from `https://api.planningcenteronline.com/`
- Parse and transpile the documentation
- Save JSON definition files to `Crews.PlanningCenter.Api/Definitions/`

## Definition File Structure

Definition files are organized by product and version:

```
Crews.PlanningCenter.Api/Definitions/
├── People/
│   ├── 2025-11-10.json
│   ├── 2025-07-17.json
│   └── ...
├── Services/
│   ├── 2018-11-01.json
│   └── ...
└── ...
```

## Source Generator

The source generator is a Roslyn incremental generator that runs at compile time.

### Configuration

In the main library project (`.csproj`):

```xml
<ItemGroup>
  <ProjectReference Include="..\Crews.PlanningCenter.Api.Generators\Crews.PlanningCenter.Api.Generators.csproj"
                    OutputItemType="Analyzer"
                    ReferenceOutputAssembly="false" />
</ItemGroup>

<ItemGroup>
  <AdditionalFiles Include="Definitions/**/*.json" />
</ItemGroup>
```

### Generated Code Location

Generated code is written to:
- `obj/Generated/` (during build)
- Namespaced by product and version

### Incremental Generation

The generator uses Roslyn's incremental generation pipeline for efficiency:
- Only regenerates when definition files change
- Caches intermediate results
- Minimizes compilation time

## Adding New Products

When Planning Center adds a new product:

1. Add the product to `ProductDefinition.All`:

```csharp
// In ProductDefinition.cs
public static readonly ProductDefinition[] All =
[
    // ... existing products
    new("new-product", "New Product", "new_product"),
];
```

2. Run the DocParser:

```bash
cd Crews.PlanningCenter.Api.DocParser
dotnet run
```

3. Rebuild the solution to generate client code

## Updating API Versions

When Planning Center releases new API versions:

1. Run the DocParser to download updated definitions:

```bash
cd Crews.PlanningCenter.Api.DocParser
dotnet run
```

2. New definition files will be added to `Definitions/{Product}/`

3. Rebuild the solution to generate clients for new versions

4. Update `LatestVersion` properties in client classes if needed

## Generated Code Structure

For each resource in a product version, the generator creates:

### Resource Class

```csharp
public record PersonResource
{
    public string Id { get; init; }
    public string Type { get; init; }
    public PersonAttributes Attributes { get; init; }
    public PersonRelationships? Relationships { get; init; }
    public ResourceLinks? Links { get; init; }
}
```

### Attributes Class

```csharp
public record PersonAttributes
{
    public string? Name { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    // ... other attributes
}
```

### Client Class

```csharp
public class PersonClient : ResourceClient<PersonResource, PersonAttributes, PersonRelationships>
{
    public PersonClient(HttpClient httpClient, Uri resourceUri)
        : base(httpClient, resourceUri) { }
}
```

## Customization

### Overriding Generated Names

Configure name overrides in DocParser's `appsettings.json`:

```json
{
  "AppSettings": {
    "DocumentationBuilder": {
      "NameOverrides": {
        "old-name": "NewName"
      }
    }
  }
}
```

### Excluding Resources

Exclude specific resources from generation:

```json
{
  "AppSettings": {
    "DocumentationBuilder": {
      "ExcludedVertices": ["deprecated-resource"]
    }
  }
}
```

## Troubleshooting

### Definition Files Not Found

Ensure definition files are marked as `AdditionalFiles` in the project:

```xml
<ItemGroup>
  <AdditionalFiles Include="Definitions/**/*.json" />
</ItemGroup>
```

### Generator Not Running

1. Clean the solution: `dotnet clean`
2. Rebuild: `dotnet build`
3. Check `obj/Generated/` for output

### Invalid JSON Definitions

If DocParser produces invalid JSON:
1. Check DocParser logs for errors
2. Verify API endpoint availability
3. Report issues with example API responses
