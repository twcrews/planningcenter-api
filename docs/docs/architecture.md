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

For each API product and version:

```
Crews.PlanningCenter.Api.{Product}.V{Version}
    ├── {Resource}Resource.cs
    ├── {Resource}Client.cs
    └── ...
```

Example:
```
Crews.PlanningCenter.Api.People.V2025_11_10
    ├── PersonResource.cs
    ├── PersonClient.cs
    ├── HouseholdResource.cs
    ├── HouseholdClient.cs
    └── ...
```

## Authentication Flow

The library provides extension methods for configuring HttpClient:

1. Consumer creates HttpClient instance
2. `.ConfigureForPlanningCenter()` sets base URL and headers
3. `.AddPlanningCenterAuth()` adds authentication handler
4. Consumer passes configured HttpClient to resource clients

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
