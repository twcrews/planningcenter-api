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

### Attributes Record

A `partial record` for the resource attributes. All attribute properties are nullable and decorated with JSON serialization attributes:

```csharp
public partial record Person
{
    [JsonPropertyName("first_name")]
    public string? FirstName { get; init; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    // ... other attributes
}
```

### Resource Record

A thin JSON:API wrapper around the attributes record:

```csharp
public partial record PersonResource : JsonApiResource<Person, PersonRelationships> { }
```

### Client Classes

Two clients are generated per resource — a singleton client for operations on a known ID, and a paginated client for collection endpoints:

```csharp
// Singleton — GET, PATCH, DELETE
public class PersonClient(HttpClient httpClient, Uri uri)
    : SingletonResourceClient<Person, PersonResource, PersonResponse>(httpClient, uri)
{
    public new Task<PersonResponse> GetAsync(CancellationToken cancellationToken = default);
    public new Task<PersonResponse> PatchAsync(Person resource, ...);
    public new Task DeleteAsync(CancellationToken cancellationToken = default);
}

// Collection — GET, POST, WithId, Filter, PerPage, Offset, Sort
public class PaginatedPersonClient(HttpClient httpClient, Uri uri)
    : PaginatedResourceClient<Person, PersonResource, PersonCollectionResponse, PersonResponse>(httpClient, uri)
{
    public new Task<PersonCollectionResponse> GetAsync(CancellationToken cancellationToken = default);
    public new Task<PersonResponse> PostAsync(Person resource, ...);
    public PersonClient WithId(string id);
    // ... filter and sort methods
}
```

### Root Product Client

A root client is generated per product, exposing versioned `OrganizationClient` instances:

```csharp
public class PeopleClient(HttpClient httpClient)
{
    public OrganizationClient Latest { get; }        // latest version
    public OrganizationClient V2025_11_10 { get; }   // specific version
    // ... other versions
}
```

## Customization

### Overriding Generated Names

Configure name overrides in DocParser's `overrides.json`:

```json
{
  "NameOverrides": [
    {
      "Product": "publishing",
      "Vertex": "episode",
      "ModelName": "Episode",
      "ResourceName": "EpisodeApiResource"
    }
  ]
}
```

### Excluding Resources

Exclude specific resources from generation:

```json
{
  "ExcludedVertices": [
    {
      "Product": "people",
      "Vertex": "birthday_people",
      "GenerateResource": false,
      "GenerateClients": true
    }
  ]
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
