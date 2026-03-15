# Architecture

Understanding the design and architecture of the Planning Center API client library.

## Overview

The library uses a two-phase code generation approach to create strongly-typed clients for the Planning Center API:

1. **Documentation Parser** - Downloads and transpiles API documentation into JSON definitions
2. **Source Generator** - Generates client code from JSON definitions at compile time

## Components

### Main Library (`Crews.PlanningCenter.Api`)

The primary library project that includes:
- Authentication helpers and extensions
- Auto-generated API client classes
- Common models and base types
- JSON definition files

**Target Framework:** .NET 8.0

### Documentation Parser (`Crews.PlanningCenter.Api.DocParser`)

A console application that:
- Fetches API metadata from Planning Center
- Transpiles documentation into structured JSON format
- Outputs definition files to the main library

**Target Framework:** .NET 10.0

### Source Generators (`Crews.PlanningCenter.Api.Generators`)

Roslyn incremental source generators that:
- Process JSON definition files at compile time
- Generate strongly-typed client and resource classes
- Support all Planning Center products and versions

**Target Framework:** .NET Standard 2.0

## Code Generation Pipeline

```
Planning Center API
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

The DocParser runs manually when you need to sync with Planning Center API updates:

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
    ├── Person                   — attributes record
    ├── PersonResource           — JSON:API resource wrapper
    ├── PersonClient             — singleton resource client (GET, PATCH, DELETE)
    ├── PaginatedPersonClient    — collection resource client (GET, POST, WithId, Filter, PerPage, Offset)
    ├── Address                  — attributes record
    ├── AddressResource
    ├── AddressClient
    ├── PaginatedAddressClient
    └── ...
```

The `OrganizationClient` exposes collections (`People`, `Households`, etc.) as `PaginatedXxxClient` instances. Calling `.WithId(id)` on a collection returns the corresponding singleton `XxxClient`.

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
- Additional helper methods for common operations
- Enhanced query parameter support
- Built-in caching strategies
- Webhooks support
- Real-time API support
