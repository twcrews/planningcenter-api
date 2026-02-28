# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a multi-project .NET solution. The primary project in the solution (`Crews.PlanningCenter.Api`) is a client library for the Planning Center API, distributed as a NuGet Package. It supports all versions of each Planning Center product.

## Project Structure

The solution contains eight projects:

1. **Crews.PlanningCenter.Api** (.NET 8.0): Main library project - includes authentication helpers and auto-generated API clients
2. **Crews.PlanningCenter.Api.Models** (.NET Standard 2.0): Shared model definitions used by both the generators and the doc parser
3. **Crews.PlanningCenter.Api.DocParser** (.NET 10.0): Console application for downloading and parsing API documentation
4. **Crews.PlanningCenter.Api.Generators** (.NET Standard 2.0): Roslyn incremental source generator for code generation
5. **Crews.PlanningCenter.Api.Tests** (.NET 10.0): Unit tests for the main library
6. **Crews.PlanningCenter.Api.DocParser.Tests** (.NET 10.0): Unit tests for the doc parser
7. **Crews.PlanningCenter.Api.Generators.Tests** (.NET 10.0): Unit tests for the source generators
8. **Crews.PlanningCenter.Api.IntegrationTests** (.NET 10.0): Integration tests that run against the live Planning Center API

The source generators are **complete and fully functional**. Client and resource classes are automatically generated at compile time from the JSON definition files in the `Definitions/` directory.

## Build and Test Commands

### Building
```bash
dotnet build
```

### Running Tests
```bash
# Run all tests
dotnet test

# Run tests for a specific project
dotnet test Crews.PlanningCenter.Api.Tests
dotnet test Crews.PlanningCenter.Api.DocParser.Tests
dotnet test Crews.PlanningCenter.Api.Generators.Tests

# Run integration tests (requires Planning Center credentials — see Integration Tests section)
dotnet test Crews.PlanningCenter.Api.IntegrationTests

# Run tests with code coverage (uses .runsettings configuration)
dotnet test --settings Crews.PlanningCenter.Api.Tests/.runsettings --collect:"XPlat Code Coverage"

# Run a specific test
dotnet test --filter "FullyQualifiedName~TestClassName.TestMethodName"
```

### Cleaning
```bash
dotnet clean
```

### Running DocParser
```bash
# Run the documentation parser to update API definitions
cd Crews.PlanningCenter.Api.DocParser
dotnet run
```

The DocParser downloads the latest API documentation from Planning Center and saves it to `Crews.PlanningCenter.Api/Definitions/`. Run this manually when you need to synchronize with Planning Center API updates.

### Generating Documentation
```bash
# Generate documentation site using DocFX
docfx docfx.json

# Serve documentation locally for preview (with hot reload)
docfx docfx.json --serve

# Generate documentation in a specific output directory
docfx docfx.json -o <output-directory>
```

The documentation is generated using [DocFX](https://dotnet.github.io/docfx/), which:
- Extracts API documentation from XML comments in the codebase
- Processes all `.csproj` files to generate API reference documentation
- Builds a static documentation site from markdown files
- Outputs to the `_site/` directory (excluded from source control)
- Uses the default and modern templates for a clean, searchable interface
- Supports PDF generation

**Configuration:** Documentation generation is configured in [docfx.json](docfx.json) at the repository root.

## Architecture

### Code Generation

The project uses a two-phase code generation approach:

#### Phase 1: Documentation Parser (`Crews.PlanningCenter.Api.DocParser`)
A console application that downloads and transpiles Planning Center API documentation into JSON definition files. This tool should be run manually when synchronizing with the latest Planning Center API updates.

**Running the DocParser:**
```bash
cd Crews.PlanningCenter.Api.DocParser
dotnet run
```

The DocParser:
- Fetches API metadata from `https://api.planningcenteronline.com/` for all supported products
- Transpiles the documentation into a structured JSON format
- Outputs definition files to `Crews.PlanningCenter.Api/Definitions/{Product}/{Version}.json`
- Supports all Planning Center products: Calendar, Check-Ins, Giving, Groups, People, Publishing, Registrations, Services

**Configuration:** The DocParser can be configured via `appsettings.json`:
```json
{
  "AppSettings": {
    "OutputDirectory": "../../../../Crews.PlanningCenter.Api/Definitions",
    "PlanningCenterClient": {
      "BaseAddress": "https://api.planningcenteronline.com/"
    }
  }
}
```

**Documentation Overrides:** Corrections for inaccuracies in the Planning Center API documentation are configured separately in `overrides.json`. This includes name overrides, excluded vertices, edge type overrides, collection overrides, and attribute type overrides. See the [DocParser README](Crews.PlanningCenter.Api.DocParser/README.md) for detailed documentation.

#### Phase 2: Incremental Source Generation (`Crews.PlanningCenter.Api.Generators`)
Contains utilities and services for generating strongly-typed client code from the JSON definition files. The generator uses [incremental source generation](https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md#syntaxvalueprovider) to efficiently process the definition files at compile time.

**Definition Files Location:**
- Path: `Crews.PlanningCenter.Api/Definitions/`
- Structure: `{Product}/{Version}.json`
- Example: `Definitions/People/2025-07-17.json`

**Note:** The source generator is configured in the main library project via:
- ProjectReference to the Generators project with `OutputItemType="Analyzer"`
- AdditionalFiles include for `Definitions/**/*.json`
- EmitCompilerGeneratedFiles is enabled to output generated code to `obj/Generated`

Client and resource classes are auto-generated at compile time and include a header comment indicating they are generated code.

### Authentication

The library provides authentication helpers that consumers can use to configure their HttpClient. Consumers control HttpClient configuration, including authentication and resilience policies.

#### Authentication Helpers

Three authentication methods are supported via extension methods:

1. **Personal Access Token** - For server-to-server integrations and development
   ```csharp
   var httpClient = new HttpClient()
       .ConfigureForPlanningCenter()
       .AddPlanningCenterAuth(new PlanningCenterPersonalAccessToken
       {
           AppId = "app-id",
           Secret = "secret"
       });
   ```

2. **OAuth Bearer Token** - When you already have an access token
   ```csharp
   var httpClient = new HttpClient()
       .ConfigureForPlanningCenter()
       .AddPlanningCenterAuth("access-token");
   ```

3. **OIDC Authentication** (Recommended for web apps) - Integrated ASP.NET Core authentication
   ```csharp
   // Configure authentication with cookie support
   builder.Services
       .AddAuthentication(options =>
       {
           options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
           options.DefaultChallengeScheme = PlanningCenterAuthenticationDefaults.AuthenticationScheme;
           options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
       })
       .AddCookie()
       .AddPlanningCenterAuthentication();  // Reads from appsettings.json automatically

   // Configure HttpClient (consumers handle token extraction from HttpContext)
   builder.Services.AddHttpClient("PlanningCenterApi", client =>
   {
       client.ConfigureForPlanningCenter();
   });
   ```

   **appsettings.json:**
   ```json
   {
     "PlanningCenter": {
       "Authority": "https://api.planningcenteronline.com",
       "ClientId": "your-client-id",
       "ClientSecret": "your-client-secret",
       "Scopes": ["openid", "people"]
     }
   }
   ```

   Note: `ClientId` and `ClientSecret` are required. `Authority` defaults to Planning Center's base URL, and `Scopes` defaults to `["openid", "people"]`.

   Alternatively, configure options manually:
   ```csharp
   builder.Services
       .AddAuthentication()
       .AddPlanningCenterAuthentication(options =>
       {
           options.ClientId = "your-client-id";
           options.ClientSecret = "your-client-secret";
       });
   ```

#### Key Classes

- **[HttpClientAuthenticationExtensions.cs](Crews.PlanningCenter.Api/Authentication/HttpClientAuthenticationExtensions.cs)** - Extension methods for configuring HttpClient with Planning Center authentication
- **[PlanningCenterAuthenticationExtensions.cs](Crews.PlanningCenter.Api/Authentication/PlanningCenterAuthenticationExtensions.cs)** - OIDC authentication setup for ASP.NET Core
- **[PlanningCenterOAuthClient.cs](Crews.PlanningCenter.Api/Authentication/PlanningCenterOAuthClient.cs)** - OAuth client for token exchange
- **[PlanningCenterOAuthClientFactory.cs](Crews.PlanningCenter.Api/Authentication/PlanningCenterOAuthClientFactory.cs)** - Factory for creating OAuth client instances
- **[PlanningCenterPersonalAccessToken.cs](Crews.PlanningCenter.Api/Authentication/PlanningCenterPersonalAccessToken.cs)** - Record struct for Personal Access Token authentication

#### Documentation

### Tests

#### Unit Tests (`Crews.PlanningCenter.Api.Tests`)
Unit tests for the main library. Uses NSubstitute for mocking and RichardSzalay.MockHttp for HTTP mocking.

#### DocParser Tests (`Crews.PlanningCenter.Api.DocParser.Tests`)
Unit tests for the documentation parser console application. Tests the parsing and transpilation logic for Planning Center API documentation.

#### Generator Tests (`Crews.PlanningCenter.Api.Generators.Tests`)
Unit tests for the incremental source generator. Tests the code generation logic that creates client and resource classes from JSON definition files.

#### Integration Tests (`Crews.PlanningCenter.Api.IntegrationTests`)
Integration tests that run against the live Planning Center API. These tests verify end-to-end behavior including CRUD operations across all supported products.

**Structure:**
- `Infrastructure/` — Shared test fixtures and base classes
  - `PlanningCenterFixture` — XUnit async fixture (`IAsyncLifetime`) that configures an authenticated `HttpClient`
  - `PlanningCenterCollection` — XUnit collection definition for sharing the fixture across test classes
  - `IntegrationTestBase` — Abstract base class providing `HttpClient` and `BaseUri` to tests
- `Products/` — Test classes organized by Planning Center product, using `[Trait("Product", "...")]` for categorization

**Credential Configuration:**
Integration tests require a Planning Center Personal Access Token. Configure via any of:
- **User secrets** (recommended for local development):
  ```bash
  cd Crews.PlanningCenter.Api.IntegrationTests
  dotnet user-secrets set "PlanningCenter:AppId" "your-app-id"
  dotnet user-secrets set "PlanningCenter:Secret" "your-secret"
  ```
- **Environment variables**: `PlanningCenter__AppId` and `PlanningCenter__Secret`
- **appsettings.json** (not recommended — avoid committing credentials)

**CI/CD:** Integration tests run in a separate GitHub Actions job using repository secrets `PLANNINGCENTER_APP_ID` and `PLANNINGCENTER_SECRET`.

## Important Patterns

### Using the API Client in ASP.NET Core

Consumers configure their own HttpClient with authentication and resilience policies:

**1. Configure HttpClient in Program.cs:**

```csharp
using Crews.PlanningCenter.Api.Authentication;

builder.Services.AddHttpClient("PlanningCenterApi", client =>
{
    client.ConfigureForPlanningCenter()
          .AddPlanningCenterAuth(new PlanningCenterPersonalAccessToken
          {
              AppId = builder.Configuration["PlanningCenter:AppId"]!,
              Secret = builder.Configuration["PlanningCenter:Secret"]!
          });
})
.AddStandardResilienceHandler(); // Optional: add .NET 8+ built-in resilience
```

**2. Use HttpClient in services:**

```csharp
public class PeopleService
{
    private readonly HttpClient _httpClient;

    public PeopleService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("PlanningCenterApi");
    }

    public async Task<PersonResource> GetPersonAsync(string personId)
    {
        var client = new PersonClient(_httpClient,
            new Uri($"/people/v2/people/{personId}", UriKind.Relative));

        var response = await client.GetAsync();
        return response.Data;
    }
}
```

### Standalone Usage (Without Dependency Injection)

For console applications or simple scenarios:

```csharp
using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.People.V2025_11_10;

var httpClient = new HttpClient()
    .ConfigureForPlanningCenter()
    .AddPlanningCenterAuth(new PlanningCenterPersonalAccessToken
    {
        AppId = "your-app-id",
        Secret = "your-secret"
    });

var client = new PersonClient(httpClient,
    new Uri("/people/v2/people/123", UriKind.Relative));

var response = await client.GetAsync();
Console.WriteLine($"Person: {response.Data?.Attributes.Name}");
```

**Best Practices:**
- Always use `IHttpClientFactory` in ASP.NET Core for proper HttpClient lifetime management
- Add resilience policies in production (`.AddStandardResilienceHandler()` or custom Polly policies)
- Store credentials securely (User Secrets for dev, environment variables for production)
- Never commit credentials to source control

See the [README.md](README.md) for complete usage examples and patterns.

### Adding New API Products
When Planning Center adds a new product API:
1. Add the new product to `ProductDefinition.All` in [ProductDefinition.cs](Crews.PlanningCenter.Api.DocParser/Models/ProductDefinition.cs)
2. Run the DocParser to download the new product's API definitions:
   ```bash
   cd Crews.PlanningCenter.Api.DocParser
   dotnet run
   ```
3. Rebuild the solution to trigger incremental source generation of the new client code

### Updating API Versions
When Planning Center releases new API versions:
1. Run the DocParser to download the updated API definitions:
   ```bash
   cd Crews.PlanningCenter.Api.DocParser
   dotnet run
   ```
2. New definition files will be added to `Crews.PlanningCenter.Api/Definitions/{Product}/`
3. Update the `LatestVersion` property in the affected client classes to point to the new version
4. Rebuild the solution to trigger incremental source generation

API versions use underscored date format (e.g., `V2025_07_17`).