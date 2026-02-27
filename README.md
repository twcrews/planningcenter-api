# .NET Planning Center API Library

A strongly-typed .NET client library for the [Planning Center API](https://developer.planning.center/docs/), featuring automatic code generation and comprehensive authentication support.

## Features

✨ **Strongly-Typed API Clients** - Auto-generated from Planning Center API documentation
🔐 **Multiple Authentication Methods** - Personal Access Token, OAuth Bearer, and OIDC
🪶 **Lightweight** - Minimal dependencies, you control HttpClient configuration
📦 **All Planning Center Products** - Calendar, Check-Ins, Giving, Groups, People, Publishing, Registrations, Services
🔄 **Version Support** - Works with all API versions

## Quick Links

- [Installation](#installation)
- [Quick Start](#quick-start)
- [Authentication](#authentication)
- [Usage Examples](#usage-examples)
- [Advanced Topics](#advanced-topics)
- [Documentation](#documentation)

## Installation

Install via NuGet:

```sh
dotnet add package Crews.PlanningCenter.Api
```

## Quick Start

### Standalone Usage

```csharp
using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.People.V2025_11_10;

// Configure HttpClient with helper methods
var httpClient = new HttpClient()
    .ConfigureForPlanningCenter()
    .AddPlanningCenterAuth(new PlanningCenterPersonalAccessToken
    {
        AppId = "your-app-id",
        Secret = "your-secret"
    });

// Use generated clients
var client = new PersonClient(httpClient,
    new Uri("/people/v2/people/123", UriKind.Relative));

var response = await client.GetAsync();
Console.WriteLine($"Person name: {response.Data?.Attributes.Name}");
```

### ASP.NET Core with Dependency Injection

```csharp
// Program.cs
using Crews.PlanningCenter.Api.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Configure HttpClient for Planning Center API
builder.Services.AddHttpClient("PlanningCenterApi", client =>
{
    client.ConfigureForPlanningCenter()
          .AddPlanningCenterAuth(new PlanningCenterPersonalAccessToken
          {
              AppId = builder.Configuration["PlanningCenter:AppId"]!,
              Secret = builder.Configuration["PlanningCenter:Secret"]!
          });
});

var app = builder.Build();
app.Run();
```

**appsettings.json:**
```json
{
  "PlanningCenter": {
    "AppId": "your-app-id",
    "Secret": "your-secret"
  }
}
```

### Using in Your Services

```csharp
using Crews.PlanningCenter.Api.People.V2025_11_10;

public class PeopleService
{
    private readonly HttpClient _httpClient;

    public PeopleService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("PlanningCenterApi");
    }

    public async Task<Person?> GetPersonAsync(string personId)
    {
        var client = new PersonClient(_httpClient,
            new Uri($"/people/v2/people/{personId}", UriKind.Relative));

        var response = await client.GetAsync();
        return response.Data?.Attributes;
    }
}
```

## Authentication

The library provides helper extension methods to configure authentication on your HttpClient.

### 1. Personal Access Token (Server-to-Server)

Best for server-to-server integrations and development:

```csharp
using Crews.PlanningCenter.Api.Authentication;

var httpClient = new HttpClient()
    .ConfigureForPlanningCenter()
    .AddPlanningCenterAuth(new PlanningCenterPersonalAccessToken
    {
        AppId = "your-app-id",
        Secret = "your-secret"
    });
```

### 2. OAuth Bearer Token

When you already have an access token:

```csharp
var httpClient = new HttpClient()
    .ConfigureForPlanningCenter()
    .AddPlanningCenterAuth("your-access-token");
```

### 3. OIDC Authentication (Web Applications)

For web applications with user authentication, add Planning Center OIDC authentication:

```csharp
using Crews.PlanningCenter.Api.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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

// Configure HttpClient for API calls
// Note: For per-request authentication, implement a custom DelegatingHandler
// to extract the token from HttpContext and add it to each request
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

**Note:** `ClientId` and `ClientSecret` are required. `Authority` defaults to Planning Center's base URL, and `Scopes` defaults to `["openid", "people"]`.

**Alternatively, configure options manually:**
```csharp
builder.Services
    .AddAuthentication()
    .AddPlanningCenterAuthentication(options =>
    {
        options.ClientId = builder.Configuration["PlanningCenter:ClientId"]!;
        options.ClientSecret = builder.Configuration["PlanningCenter:ClientSecret"]!;
        // Override other settings as needed
    });
```

### Authentication Helpers Reference

- **`ConfigureForPlanningCenter()`** - Sets base URL and JSON:API accept header
- **`AddPlanningCenterAuth(PlanningCenterPersonalAccessToken)`** - Adds PAT authentication
- **`AddPlanningCenterAuth(string bearerToken)`** - Adds OAuth bearer token authentication

## Adding Resilience Policies

You control HttpClient configuration, including resilience policies. Add retry, circuit breaker, and timeout policies using .NET's built-in resilience handler or Polly.

### Option 1: .NET 8+ Built-in Resilience (Recommended)

```csharp
builder.Services.AddHttpClient("PlanningCenterApi", client =>
{
    client.ConfigureForPlanningCenter()
          .AddPlanningCenterAuth(token);
})
.AddStandardResilienceHandler(); // Built-in retry, circuit breaker, timeout
```

### Option 2: Custom Polly Policies

First, add the Polly package:

```sh
dotnet add package Microsoft.Extensions.Http.Polly
```

Then configure policies:

```csharp
using Polly;
using Polly.Extensions.Http;

builder.Services.AddHttpClient("PlanningCenterApi", client =>
{
    client.ConfigureForPlanningCenter()
          .AddPlanningCenterAuth(token);
})
.AddTransientHttpErrorPolicy(policy =>
    policy.WaitAndRetryAsync(3, retryAttempt =>
        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
.AddTransientHttpErrorPolicy(policy =>
    policy.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
```

## Usage Examples

### Fetching a Single Resource

```csharp
public async Task<Person?> GetPersonAsync(string personId)
{
    var client = new PersonClient(_httpClient,
        new Uri($"/people/v2/people/{personId}", UriKind.Relative));

    try
    {
        var response = await client.GetAsync();
        return response.Data?.Attributes;
    }
    catch (JsonApiException ex) when (ex.StatusCode == 404)
    {
        return null;
    }
}
```

### Fetching a Collection

```csharp
public async Task<IEnumerable<Person>> GetPeopleAsync(int limit = 25)
{
    var client = new PeopleClient(_httpClient,
        new Uri("/people/v2/people", UriKind.Relative))
        .Take(limit);

    var response = await client.GetAsync();
    return response.Data.Select(r => r.Attributes);
}
```

### Creating, Updating, and Deleting Resources

```csharp
// POST - Create a new resource
var newPerson = new Person { FirstName = "John", LastName = "Doe" };
var createClient = new PeopleClient(_httpClient,
    new Uri("/people/v2/people", UriKind.Relative));
var createResponse = await createClient.PostAsync(newPerson);

// PATCH - Update an existing resource
var updatePerson = new Person { FirstName = "Jane" };
var updateClient = new PersonClient(_httpClient,
    new Uri($"/people/v2/people/{personId}", UriKind.Relative));
var updateResponse = await updateClient.PatchAsync(updatePerson);

// DELETE - Remove a resource
var deleteClient = new PersonClient(_httpClient,
    new Uri($"/people/v2/people/{personId}", UriKind.Relative));
await deleteClient.DeleteAsync();
```

## Advanced Topics

### Querying and Filtering

Use fluent methods to filter, sort, and include related resources:

```csharp
// Include related resources
var client = new PeopleClient(_httpClient,
    new Uri("/people/v2/people", UriKind.Relative))
    .Include("emails", "phone_numbers")
    .Take(25);

var response = await client.GetAsync();

// Sort results
var sortedClient = new PeopleClient(_httpClient,
    new Uri("/people/v2/people", UriKind.Relative))
    .OrderBy("last_name")
    .Take(100);

// Filter with query parameters
var filteredClient = new PeopleClient(_httpClient,
    new Uri("/people/v2/people", UriKind.Relative))
    .Where("first_name", "John")
    .Take(50);
```

### Pagination

Handle pagination with offset and limit:

```csharp
public async Task<IEnumerable<Person>> GetAllPeopleAsync()
{
    var allPeople = new List<Person>();
    int offset = 0;
    const int pageSize = 100;
    bool hasMore = true;

    while (hasMore)
    {
        var client = new PeopleClient(_httpClient,
            new Uri("/people/v2/people", UriKind.Relative))
            .Skip(offset)
            .Take(pageSize);

        var response = await client.GetAsync();
        allPeople.AddRange(response.Data.Select(r => r.Attributes));

        offset += pageSize;
        hasMore = response.Data.Count == pageSize;
    }

    return allPeople;
}
```

### Error Handling

Handle Planning Center API errors:

```csharp
using Crews.PlanningCenter.Api.Exceptions;

try
{
    var response = await client.GetAsync();
}
catch (JsonApiException ex) when (ex.StatusCode == 404)
{
    // Resource not found
    Console.WriteLine("Resource not found");
}
catch (JsonApiException ex) when (ex.StatusCode == 429)
{
    // Rate limit exceeded
    var retryAfter = ex.RetryAfter ?? TimeSpan.FromSeconds(60);
    await Task.Delay(retryAfter);
    // Retry the request
}
catch (JsonApiException ex)
{
    // Other API errors
    Console.WriteLine($"API Error: {ex.StatusCode} - {ex.Message}");
}
```

### Working with Multiple API Versions

Each product client supports multiple API versions:

```csharp
using Crews.PlanningCenter.Api.People.V2024_09_01;
using Crews.PlanningCenter.Api.People.V2025_11_10;

// Use a specific version
var oldClient = new V2024_09_01.PersonClient(_httpClient, uri);
var newClient = new V2025_11_10.PersonClient(_httpClient, uri);
```

## Limitations

This library aims to be flexible and covers nearly all API use cases. However, there are a few exceptions.

### Household Creation

Creating a new household in the `People` API requires assigning membership via relationships in the outbound JSON:API document. The built-in `PostAsync` methods of this library do not support sending documents with relationship data.

Here's the workaround:

```cs
// Get the households URL
Uri uri = PeopleClient.Latest.Households.Uri;

// Create your document
JsonApiDocument<HouseholdResource> document = new()
{
    Data = new()
    {
        Attributes = new()
        {
            Name = "Smith Household"
        },
        Relationships = new()
        {
            People = new()
            {
                // Use an identifier for each member's person ID
                Data = [ new() { Id = 1 }, new() { Id = 2 } ]
            },
            PrimaryContact = new()
            {
                // Primary contact's person ID
                Data = new() { Id = 3 }
            }
        }
    }
}

// Prepare the document
StringContent content = new(JsonSerializer.Serialize(document));

// Send the request manually
HttpResponseMessage response = await myHttpClient.SendAsync(new(uri, content));
```

## Documentation

### Detailed Guides

- **[CLAUDE.md](CLAUDE.md)** - Developer guide for contributing to this project
- **[MIGRATION_V3.md](MIGRATION_V3.md)** - Migration guide from v2.x to v3.0

### External Resources

- [Planning Center API Documentation](https://developer.planning.center/docs/)
- [Planning Center API Explorer](https://api.planningcenteronline.com/)
- [JSON:API Specification](https://jsonapi.org/)

## Supported Products

All Planning Center products are supported with auto-generated clients:

- **Calendar** - Event scheduling and room booking
- **Check-Ins** - Event check-in management
- **Giving** - Donation tracking and management
- **Groups** - Small group organization
- **People** - Contact and member management
- **Publishing** - Content publishing workflows
- **Registrations** - Event registration forms
- **Services** - Service planning and scheduling

Each product supports multiple API versions, with clients auto-generated for each version.

## Best Practices

1. **Use IHttpClientFactory** - Always use `IHttpClientFactory` in ASP.NET Core applications for proper HttpClient lifetime management
2. **Add Resilience Policies** - Use `.AddStandardResilienceHandler()` or Polly policies in production for retry and circuit breaker support
3. **Store Credentials Securely** - Use User Secrets for development and environment variables or Key Vault for production
4. **Handle Rate Limits** - Planning Center APIs have rate limits; implement appropriate retry logic with exponential backoff
5. **Use Specific API Versions** - Pin to specific API versions for stability; test before upgrading

## Testing

Try the library quickly with the included Sandbox application:

```bash
cd Crews.PlanningCenter.Api.Sandbox
dotnet run
```

See the [Sandbox README](Crews.PlanningCenter.Api.Sandbox/README.md) for setup instructions.

## Contributing

Contributions are welcome! Please read [CLAUDE.md](CLAUDE.md) for development guidelines and project architecture.

## License

This project is open source. See the repository for license details.

***

> S.D.G.
