# Migration Guide: v2.x to v3.0

This guide helps you migrate from v2.x (which used DI registration and included Polly) to v3.0 (consumer-provided HttpClient approach).

## Overview of Changes

Version 3.0 simplifies the library by removing opinionated infrastructure code and giving consumers full control over HttpClient configuration. This makes the library lighter-weight and more flexible.

### What Changed

**Removed:**
- `AddPlanningCenterApi()` extension methods
- `AddPlanningCenterApiWithBearer()` extension method
- `AddPlanningCenterApiWithDefaultAuth()` extension method
- `AddStandardResiliencePolicies()` and Polly integration
- `PlanningCenterApiOptions` configuration class
- `PlanningCenterApiServiceCollectionExtensions` class
- `PlanningCenterApiHttpClientBuilderExtensions` class
- 4 NuGet package dependencies (Microsoft.Extensions.DependencyInjection, Microsoft.Extensions.Http, Microsoft.Extensions.Http.Polly, Microsoft.Extensions.Options)

**Added:**
- `ConfigureForPlanningCenter()` extension method on HttpClient
- `AddPlanningCenterAuth(PlanningCenterPersonalAccessToken)` extension method on HttpClient
- `AddPlanningCenterAuth(string bearerToken)` extension method on HttpClient
- `PlanningCenterOAuthClientFactory` for creating OAuth clients
- `HttpClientAuthenticationExtensions` class with helper methods

**Unchanged:**
- Generated client classes (still accept HttpClient via constructor)
- OIDC authentication setup (`AddPlanningCenterAuthentication`)
- All authentication types and OAuth client functionality

## Breaking Changes

### 1. HttpClient Registration

**Before (v2.x):**
```csharp
// Library registered HttpClient with DI
builder.Services
    .AddPlanningCenterApi("app-id", "secret")
    .AddStandardResiliencePolicies();

// Access via factory
var httpClient = httpClientFactory.CreateClient(
    PlanningCenterApiServiceCollectionExtensions.HttpClientName);
```

**After (v3.0):**
```csharp
using Crews.PlanningCenter.Api.Authentication;

// You register HttpClient with your own configuration
builder.Services.AddHttpClient("PlanningCenterApi", client =>
{
    client.ConfigureForPlanningCenter()
          .AddPlanningCenterAuth(new PlanningCenterPersonalAccessToken
          {
              AppId = builder.Configuration["PlanningCenter:AppId"]!,
              Secret = builder.Configuration["PlanningCenter:Secret"]!
          });
});

// Access via factory (same pattern)
var httpClient = httpClientFactory.CreateClient("PlanningCenterApi");
```

### 2. Resilience Policies

**Before (v2.x):**
```csharp
builder.Services
    .AddPlanningCenterApi("app-id", "secret")
    .AddStandardResiliencePolicies(); // Library provided
```

**After (v3.0) - Option 1: .NET 8+ Built-in Resilience (Recommended):**
```csharp
builder.Services.AddHttpClient("PlanningCenterApi", client =>
{
    client.ConfigureForPlanningCenter()
          .AddPlanningCenterAuth(token);
})
.AddStandardResilienceHandler(); // Built-in .NET 8+ resilience
```

**After (v3.0) - Option 2: Custom Polly Policies:**
```csharp
// First, add Polly package to your project:
// dotnet add package Microsoft.Extensions.Http.Polly

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

### 3. Bearer Token Authentication

**Before (v2.x):**
```csharp
builder.Services.AddPlanningCenterApiWithBearer("your-access-token");
```

**After (v3.0):**
```csharp
builder.Services.AddHttpClient("PlanningCenterApi", client =>
{
    client.ConfigureForPlanningCenter()
          .AddPlanningCenterAuth("your-access-token");
});
```

### 4. OIDC Authentication with API Client

**Before (v2.x):**
```csharp
// Add OIDC authentication
builder.Services.AddPlanningCenterAuthentication(options =>
{
    options.ClientId = "client-id";
    options.ClientSecret = "client-secret";
    options.Scopes = PlanningCenterOAuthScope.OpenId | PlanningCenterOAuthScope.People;
});

// Add API client (library handled token extraction)
builder.Services.AddPlanningCenterApiWithDefaultAuth();
```

**After (v3.0):**
```csharp
// Add OIDC authentication (unchanged)
builder.Services.AddPlanningCenterAuthentication(options =>
{
    options.ClientId = "client-id";
    options.ClientSecret = "client-secret";
    options.Scopes = PlanningCenterOAuthScope.OpenId | PlanningCenterOAuthScope.People;
});

// Configure HttpClient (you handle token extraction)
builder.Services.AddHttpClient("PlanningCenterApi", client =>
{
    client.ConfigureForPlanningCenter();
    // Note: You need to implement a custom DelegatingHandler to extract
    // the token from HttpContext and add it to each request.
});

// Example custom handler:
public class AuthTokenHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthTokenHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var accessToken = await _httpContextAccessor.HttpContext
            .GetTokenAsync("access_token");

        if (!string.IsNullOrEmpty(accessToken))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}

// Register the handler:
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<AuthTokenHandler>();
builder.Services.AddHttpClient("PlanningCenterApi", client =>
{
    client.ConfigureForPlanningCenter();
})
.AddHttpMessageHandler<AuthTokenHandler>();
```

### 5. Standalone Usage

**Before (v2.x):**
```csharp
var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://api.planningcenteronline.com")
};

var token = new PlanningCenterPersonalAccessToken
{
    AppId = "app-id",
    Secret = "secret"
};
httpClient.DefaultRequestHeaders.Authorization = token;

// Add JSON:API accept header
httpClient.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.api+json"));
```

**After (v3.0):**
```csharp
using Crews.PlanningCenter.Api.Authentication;

var httpClient = new HttpClient()
    .ConfigureForPlanningCenter()
    .AddPlanningCenterAuth(new PlanningCenterPersonalAccessToken
    {
        AppId = "app-id",
        Secret = "secret"
    });
```

### 6. OAuth Client Creation

**Before (v2.x):**
```csharp
builder.Services.AddPlanningCenterOAuthClient(options =>
{
    options.ClientId = "client-id";
    options.ClientSecret = "client-secret";
    options.RedirectUri = "https://example.com/callback";
});

// Inject PlanningCenterOAuthClient via DI
```

**After (v3.0):**
```csharp
using Crews.PlanningCenter.Api.Authentication;

var options = new PlanningCenterOAuthClientOptions
{
    ClientId = "client-id",
    ClientSecret = "client-secret",
    RedirectUri = "https://example.com/callback"
};

// Option 1: Use factory with default HttpClient
var oauthClient = PlanningCenterOAuthClientFactory.Create(options);

// Option 2: Use factory with custom HttpClient
var httpClient = new HttpClient();
var oauthClient = PlanningCenterOAuthClientFactory.Create(httpClient, options);

// Option 3: Direct instantiation
var oauthClient = new PlanningCenterOAuthClient(httpClient, options);
```

## Migration Checklist

- [ ] Remove calls to `AddPlanningCenterApi()`, `AddPlanningCenterApiWithBearer()`, and `AddPlanningCenterApiWithDefaultAuth()`
- [ ] Replace with `AddHttpClient()` configuration using `ConfigureForPlanningCenter()` and `AddPlanningCenterAuth()`
- [ ] If using resilience policies, add `.AddStandardResilienceHandler()` or custom Polly policies
- [ ] If using OIDC with API calls, implement custom DelegatingHandler for token extraction
- [ ] Replace `AddPlanningCenterOAuthClient()` with `PlanningCenterOAuthClientFactory.Create()` if using OAuth client
- [ ] Update any references to `PlanningCenterApiServiceCollectionExtensions.HttpClientName` with your own client name
- [ ] Test authentication and API calls to ensure they work correctly
- [ ] Remove any references to `PlanningCenterApiOptions` (no longer exists)

## Benefits of Migrating

1. **Lighter Dependencies** - Removes 4 package dependencies (saves ~500KB)
2. **More Control** - You choose your own resilience strategy (.NET 8+ built-in, Polly, or custom)
3. **Simpler API** - No magic configuration, just provide HttpClient
4. **Better Separation** - Library focuses on Planning Center API, not infrastructure concerns
5. **Modern Patterns** - Follows .NET 8+ best practices for HttpClient resilience
6. **Flexibility** - Easy to customize authentication, timeouts, headers, and policies

## Need Help?

- [README.md](README.md) - Complete usage examples
- [CLAUDE.md](CLAUDE.md) - Project architecture and patterns
- [GitHub Issues](https://github.com/twcrews/planningcenter-api/issues) - Report problems or ask questions

## Version History

- **v2.x** - DI registration with Polly integration (deprecated)
- **v3.0** - Consumer-provided HttpClient approach (current)
