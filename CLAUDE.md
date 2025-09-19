# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Common Development Commands

### Building and Testing
- `dotnet build` - Build the entire solution
- `dotnet test` - Run all unit tests
- `dotnet test --collect:"XPlat Code Coverage"` - Run tests with code coverage
- `dotnet pack` - Create NuGet packages

### Code Generation
- `./Crews.PlanningCenter.Api.Generators/generator.sh` - Regenerate all API resource classes, client classes, and document contexts using T4 templates
- The generator script builds the project first, then uses T4 templates to generate code based on Planning Center API documentation

### Project Structure Commands
- Build generators project: `dotnet build Crews.PlanningCenter.Api.Generators/`
- Build main library: `dotnet build Crews.PlanningCenter.Api/`
- Build tests: `dotnet build Crews.PlanningCenter.Api.Tests/`

## Architecture Overview

This is a .NET 9 C# library that provides a strongly-typed client for the Planning Center API, built on top of the JSON:API Framework.

### Core Architecture Components

**Three-Project Structure:**
1. **Crews.PlanningCenter.Api** - Main library with generated API clients and resources
2. **Crews.PlanningCenter.Api.Generators** - T4 template-based code generators that read API documentation and generate typed resources
3. **Crews.PlanningCenter.Api.Tests** - xUnit test project with comprehensive coverage

**Generated Code Pattern:**
- Most API client classes (in `Clients/` folder) are auto-generated from T4 templates
- Resource classes (in `Resources/` folder) are versioned by API version (e.g., `V2018_08_01`, `V2025_07_17`) and auto-generated
- Current latest versions include: CheckIns `V2025_05_28`, People `V2025_07_17`, Publishing `V2024_03_25`, Calendar `V2022_07_07`, Groups `V2023_07_10`, Giving `V2019_10_18`, Services `V2018_11_01`
- Document contexts per API service are auto-generated
- Generation process uses reflection and API documentation parsing

**Key Base Classes:**
- `PlanningCenterDocumentContext` - Base JSON:API document context with Planning Center conventions
- `PlanningCenterFetchableResource` - Base class for API resources with fluent query methods
- Various resource interfaces (`IFilterable`, `IIncludable`, `IOrderable`, `IQueryable`) for fluent API support

**API Clients:**
- Service-specific clients: `CalendarClient`, `CheckInsClient`, `GivingClient`, `GroupsClient`, `PeopleClient`, `PublishingClient`, `ServicesClient`
- Each client provides version-specific resource access (e.g., `LatestVersion` property)
- Fluent API design for chaining resource calls and queries

**Dependency Injection Support:**
- `AddPlanningCenterApi()` extension method for service registration
- `AddPlanningCenterOAuth()` extension methods for OAuth authentication (in `DependencyInjection` namespace)
- Options pattern with `PlanningCenterApiOptions`
- Support for both configuration-based and programmatic setup
- OAuth configuration uses `PlanningCenter:ClientId` and `PlanningCenter:ClientSecret` sections by default

### Key Conventions

**Naming:**
- Snake case for API attribute names (handled by `SnakeCaseNamingConvention`)
- Pascal case for type names
- Versioned namespaces follow pattern `V{YYYY}_{MM}_{DD}`

**Authentication:**
- Uses `PlanningCenterPersonalAccessToken` record struct (AppID + Secret) in `Authentication` namespace (moved to Authentication namespace in v1.2.0)
- Supports OAuth via `PlanningCenterOAuthDefaults` class and related OAuth components (added in v1.2.0)
- Includes `PlanningCenterOAuthHandler`, `PlanningCenterOAuthOptions`, `PlanningCenterClaimsTransformation` for comprehensive OAuth support
- Personal Access Token automatically converted to HTTP Basic Authorization header
- OAuth constants include endpoints for authorization, token exchange, and user information

**Fluent Query API:**
- Include related resources: `.Include(ResourceIncludable.RelatedResource)`
- Filter collections: `.Query((QueryField, "value"))`  
- Order results: `.OrderBy(OrderableField, Order.Ascending/Descending)`
- Pagination: `.GetAllAsync(count: 10, offset: 5)`

## Important Notes

**Generated Code:**
- Files in `Clients/`, most of `Resources/`, and document contexts are auto-generated
- Do not modify generated files directly - they will be overwritten
- To modify generated code behavior, update the T4 templates in `Generators/Templates/`

**Testing:**
- Uses xUnit with NSubstitute for mocking
- Code coverage configured to exclude generated files (see `.runsettings`)
- MockHttp used for HTTP testing scenarios

**Versioning:**
- Library follows semantic versioning (currently v1.2.0, with v2.0.0 released September 2025)
- API resource versions match Planning Center's API versioning scheme
- Latest API versions are aliased via `LatestVersion` properties
- v2.0.0 (September 2025): Fixed People related resource properties, moved OAuth extension methods to DependencyInjection namespace, simplified OAuth configuration
- v1.2.0 (August 2025): Added OAuth support and comprehensive API regeneration
- Upgraded to .NET 9 with major dependency updates: upgraded to Crews.Extensions.Http 2.0.0, Crews.PlanningCenter.Models 1.2.0, and Microsoft.Extensions packages 9.0.1

**Code Generation Process:**
1. Build the main library and generators projects
2. Run T4 templates that use reflection and API documentation parsing
3. Generate client classes, resource classes, and document contexts
4. Generated files include header comments indicating they're auto-generated