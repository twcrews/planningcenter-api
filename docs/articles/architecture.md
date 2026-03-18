# Architecture

This article covers the design and architecture of the library.

## Overview

The library uses a two-phase code generation approach to create strongly-typed clients for the Planning Center API:

1. **Documentation Parser** - Downloads and transpiles API documentation into JSON definitions
2. **Source Generator** - Generates client code from JSON definitions at compile time

## Components

### Main Library (`Crews.PlanningCenter.Api`)

The primary library project that includes everything in the NuGet package:
- Authentication helpers and extensions
- Auto-generated API client classes
- Common models and base types
- JSON definition files

**Target Framework:** .NET 8.0

### Documentation Parser (`Crews.PlanningCenter.Api.DocParser`)

A console application that:
- Fetches API metadata from Planning Center
- Transpiles documentation into structured JSON format
- Applies overrides where necessary (i.e. errors or typos in documentation)
- Outputs definition files to the main library

**Target Framework:** .NET 10.0

### Source Generators (`Crews.PlanningCenter.Api.Generators`)

Roslyn incremental source generators that:
- Process JSON definition files at compile time
- Generate strongly-typed client and resource classes

**Target Framework:** .NET Standard 2.0

## Code Generation Pipeline

```
Planning Center API Docs
        ↓
[DocParser Downloads]
        ↓
JSON Definitions
        ↓
[Source Generator]
        ↓
Generated Client Code
```

### Phase 1: Documentation Parsing

The DocParser runs manually when syncrhonization with Planning Center docs is needed:

```bash
cd Crews.PlanningCenter.Api.DocParser
dotnet run
```

Output: `Crews.PlanningCenter.Api/Definitions/{Product}/{Version}.json`

### Phase 2: Source Generation

The source generator runs automatically at compile time:

1. Reads JSON definition files via `AdditionalFiles`
2. Uses incremental generation for efficiency
3. Outputs generated code to `obj/Generated`
4. Makes generated types available to the compiler

## Generated Code Structure

For each API product, the generator creates a root client in the `Crews.PlanningCenter.Api` namespace:

```csharp
// Root product client — one per product, exposes versioned OrganizationClient instances
public class PeopleClient(HttpClient httpClient)
{
    public OrganizationClient Latest { get; }       // latest version (sets X-PCO-API-Version header)
    public OrganizationClient V2025_11_10 { get; }  // specific version
    // ... other versions
}
```

For each product version, resource types are generated in their versioned namespace:

```
Crews.PlanningCenter.Api.People.V2025_11_10
    ├── OrganizationClient       — root entry point for this version
    ├── Person                   — Core model; contains resource attributes
    ├── PersonResource           — Resource model; contains ID and type
    ├── PersonClient             — singleton client (GET, PATCH, DELETE)
    ├── PaginatedPersonClient    — collection client (GET, POST, WithId, etc.)
    ├── Address                  — ...
    ├── AddressResource          - ...
    ├── AddressClient            - ...
    ├── PaginatedAddressClient   - ...
    └── ...
```

The `OrganizationClient` exposes collections (`People`, `Households`, etc.) as `PaginatedXxxClient` instances. Calling `.WithId(id)` on a collection returns the corresponding singleton `XxxClient`.

## Response Objects

Every client operation returns a strongly-typed response object. The generator produces two response types per resource:

| Type | Returned by | `Data` type |
|------|-------------|-------------|
| `PersonResponse` | Singleton `GetAsync`, `PatchAsync`, `PostAsync` | `PersonResource?` |
| `PersonCollectionResponse` | Paginated `GetAsync` | `IEnumerable<PersonResource>?` |

Both types inherit from `ResourceResponse<T>`, which exposes three properties:

```csharp
public abstract class ResourceResponse<T>
{
    public T? Data { get; init; }                      // deserialized primary data
    public JsonApiDocument? ResponseBody { get; init; } // full parsed JSON:API document
    public HttpResponseMessage? ResponseMessage { get; init; } // raw HTTP response
}
```

### `Data`

The primary resource data, deserialized to the appropriate `*Resource` type (or `IEnumerable<*Resource>` for collections). This is sufficient for most use cases:

```csharp
var response = await peopleClient.People.WithId("123").GetAsync();
Console.WriteLine(response.Data?.Attributes?.Name);
```

### `ResponseBody`

The full `JsonApiDocument` from the `Crews.Web.JsonApiClient` library. Use this to access sideloaded resources, pagination links, or metadata that the typed `Data` property does not surface:

```csharp
// Access sideloaded resources (requires a prior Include*() call)
var included = response.ResponseBody?.Included;

// Access pagination links
var nextLink = response.ResponseBody?.Links?["next"];

// Access document-level metadata
var meta = response.ResponseBody?.Meta;
```

### `ResponseMessage`

The underlying `HttpResponseMessage`. Use this for low-level inspection such as status codes or response headers:

```csharp
var statusCode = response.ResponseMessage?.StatusCode;
var rateLimit = response.ResponseMessage?.Headers
    .GetValues("X-PCO-API-Request-Rate-Limit")
    .FirstOrDefault();
```

## Authentication Flow

Consumers own their HttpClient configuration. The library provides authentication value types and OIDC extensions, but does not manage the HttpClient itself:

1. Consumer creates and configures `HttpClient` (base address, Accept header, Authorization header)
2. Consumer sets `Authorization` to a `PlanningCenterPersonalAccessToken` (implicitly converts to a Basic auth header) or uses OIDC via `AddPlanningCenterAuthentication()`
3. Consumer constructs a root product client (e.g., `new PeopleClient(httpClient)`) and navigates the hierarchy

## Design Principles

- **Consumer Control** - Consumers manage HttpClient lifetime and configuration
- **Strongly Typed** - All API resources and operations are strongly typed
- **Version Support** - Multiple API versions coexist in separate namespaces
- **Incremental Generation** - Efficient compile-time code generation
- **Minimal Dependencies** - Keep runtime dependencies minimal

## Future Enhancements

Potential areas for future development:
- Additional helper methods for common operations (i.e. get first ID from a collection response)
- Built-in caching strategies
- Webhooks support
- Rate limit handling
