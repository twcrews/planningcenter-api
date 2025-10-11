# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a .NET library that provides a client for the Planning Center API, built on top of the JSON:API Framework. It supports all major Planning Center products (Calendar, Check-Ins, Giving, Groups, People, Publishing, Services) with API versioning support.

The library uses ASP.NET Core's authentication system to support both:
- Personal Access Token authentication (from configuration)
- OAuth/OpenID Connect authentication (from authenticated user context)

## Build and Test Commands

### Building
```bash
dotnet build
```

### Running Tests
```bash
# Run all tests (unit + integration)
dotnet test

# Run only unit tests (fast, no credentials needed)
dotnet test --filter "FullyQualifiedName~Crews.PlanningCenter.Api.Tests"

# Run only integration tests (slow, requires credentials)
dotnet test --filter "FullyQualifiedName~IntegrationTests"

# Run tests with code coverage (uses .runsettings configuration)
dotnet test --settings Crews.PlanningCenter.Api.Tests/.runsettings --collect:"XPlat Code Coverage"

# Run a specific test
dotnet test --filter "FullyQualifiedName~TestClassName.TestMethodName"
```

### Cleaning
```bash
dotnet clean
```

## Architecture

### Code Generation
The `Crews.PlanningCenter.Api.Generators` project contains utilities for generating client code from Planning Center's API documentation. The `PlanningCenterApiReferenceService` fetches API metadata from `https://api.planningcenteronline.com/` and generates strongly-typed resource classes.

**Note:** Client classes (in `Clients/` folder) and resource classes (in `Resources/` folder) are auto-generated. They include a header comment: `This code is automatically generated. Please do not modify it directly.`

### Resource Model Pattern

The library uses a sophisticated resource pattern with two main base classes:

1. **PlanningCenterSingletonFetchableResource**: Represents a single resource that can be fetched, posted, patched, or deleted
2. **PlanningCenterPaginatedFetchableResource**: Represents a collection of resources that can be fetched with pagination, filtering, ordering, and querying

All resource types are generic and require:
- `TResource`: The entity model type
- `TContext`: A Planning Center document context for JSON:API serialization
- `TSelf`: Self-referential type for fluent chaining

Custom resources can be created by inheriting from these base classes and implementing the appropriate interfaces (`IIncludable`, `IOrderable`, `IFilterable`).

### Authentication Flow

1. **AddPlanningCenter()**: Registers OpenID Connect authentication with Planning Center
   - Configures authority, scopes, and claims transformation
   - Saves OAuth tokens for later use
   - Located in [AuthenticationBuilderExtensions.cs](Crews.PlanningCenter.Api/DependencyInjection/AuthenticationBuilderExtensions.cs)

2. **AddPlanningCenterClient()**: Registers the API client with DI
   - Configures `IPlanningCenterClient` as a scoped service
   - Registers `PlanningCenterAuthenticationHandler` as a delegating handler
   - Located in [ServiceCollectionExtensions.cs](Crews.PlanningCenter.Api/DependencyInjection/ServiceCollectionExtensions.cs)

3. **PlanningCenterAuthenticationHandler**: Automatic authentication injection
   - Prioritizes Personal Access Token from configuration if available
   - Falls back to OAuth access token from `HttpContext` if authenticated
   - Allows per-request authorization header overrides
   - Located in [Authentication/PlanningCenterAuthenticationHandler.cs](Crews.PlanningCenter.Api/Authentication/PlanningCenterAuthenticationHandler.cs)

4. **PlanningCenterClaimsTransformation**: Maps Planning Center claims to .NET conventions
   - Transforms OAuth claims into standard ASP.NET Core claim types
   - Located in [Authentication/PlanningCenterClaimsTransformation.cs](Crews.PlanningCenter.Api/Authentication/PlanningCenterClaimsTransformation.cs)

### Client Architecture

The `IPlanningCenterClient` interface provides access to all Planning Center product APIs:
- Calendar, CheckIns, Giving, Groups, People, Publishing, Services

Each client (e.g., `PeopleClient`) provides access to multiple API versions via properties like:
- `LatestVersion`: Points to the most recent API version
- `V2025_07_17`, `V2024_09_12`, etc.: Specific API versions

Each version property returns an `OrganizationResource` which serves as the entry point to that API version's resource hierarchy.

### Configuration

Configuration uses the options pattern with two main sections:

1. **Authentication:PlanningCenter**: OpenID Connect configuration
   - `ClientId`, `ClientSecret`, `Scope[]`
   - Bound to `OpenIdConnectOptions`

2. **PlanningCenterClient**: API client configuration
   - `ApiBaseAddress`: Default is `https://api.planningcenteronline.com`
   - `HttpClientName`: Default is `__defaultPlanningCenterClient`
   - `UserAgent`: Required by Planning Center API
   - `PersonalAccessToken`: Optional PAT with `AppId` and `Secret`

See [PlanningCenterClientOptions.cs](Crews.PlanningCenter.Api/DependencyInjection/PlanningCenterClientOptions.cs) and README.md for details.

### Fluent API Pattern

The library uses method chaining for building API requests:

```csharp
var result = await client.People.LatestVersion
    .GetRelated<PersonResourceCollection>("people")
    .Include(PersonIncludable.Emails)
    .OrderBy(PersonOrderable.LastName, Order.Ascending)
    .Query((PersonQueryable.FirstName, "John"))
    .GetAllAsync(count: 25, offset: 0);
```

Resources use the following pattern:
- Collections provide `.WithID(id)` to get singleton resources
- Singleton resources provide navigation properties to related resources
- Both support `.GetRelated<T>(path)` for accessing custom or undocumented resources

### Test Architecture

The project has two test suites:

#### Unit Tests (`Crews.PlanningCenter.Api.Tests`)
Fast, isolated tests using mocks and fakes. Organized by namespace matching the main project:
- `Authentication/`: Authentication handler and claims transformation tests
- `DependencyInjection/`: Service registration tests
- `Extensions/`: Extension method tests
- `Models/`: Resource model tests
- `Dummies/`: Test fixtures and dummy data

Code coverage excludes auto-generated code (Clients, Resources folders) and some extension methods.

#### Integration Tests (`Crews.PlanningCenter.Api.IntegrationTests`)
Tests that make real API calls to Planning Center. **Requires valid credentials** to run.

**Configuration:** Integration tests require Planning Center API credentials via user secrets or environment variables:
```bash
# Using user secrets (recommended for local development)
cd Crews.PlanningCenter.Api.IntegrationTests
dotnet user-secrets set "PlanningCenterClient:PersonalAccessToken:AppId" "your-app-id"
dotnet user-secrets set "PlanningCenterClient:PersonalAccessToken:Secret" "your-secret"

# Using environment variables (recommended for CI/CD)
export PLANNINGCENTER_AppId="your-app-id"
export PLANNINGCENTER_Secret="your-secret"
```

Test organization:
- `Infrastructure/`: Base classes, fixtures, and helpers
- `Authentication/`: Personal Access Token and OAuth integration tests
- `Clients/`: Product-specific API client tests (People, Calendar, etc.)
- `Features/`: Feature tests (fluent API, pagination, includes, etc.)

See [Integration Tests README](Crews.PlanningCenter.Api.IntegrationTests/README.md) for detailed documentation.

## Important Patterns

### Adding New API Products
When Planning Center adds a new product API, add it to the `Products` list in `PlanningCenterApiReferenceService.cs:24-33` and regenerate client code.

### API Version Support
Each client supports multiple API versions. The `LatestVersion` property should be updated when new API versions are released. Versions use underscored date format (e.g., `V2025_07_17`).

### Vertex Type Overrides
Some API documentation has incorrect type information. Override these in `VertexTypeOverrides` dictionary in [PlanningCenterApiReferenceService.cs](Crews.PlanningCenter.Api.Generators/Services/PlanningCenterApiReferenceService.cs:15-22).

### Resource Capabilities
Resources declare their capabilities via interfaces:
- `IIncludable<TSelf, TEnum>`: Can include related resources
- `IOrderable<TSelf, TEnum>`: Can order results
- `IFilterable<TSelf, TEnum>`: Can filter results
- `IDeletableResource`: Can be deleted
- `IPatchableResource<TResource>`: Can be updated
- `IPostableResource<TResource>`: Can be created

Check the Planning Center API documentation to determine which capabilities each resource supports.
