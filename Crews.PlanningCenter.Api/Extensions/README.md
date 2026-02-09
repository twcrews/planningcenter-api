# Extensions Directory

This directory previously contained dependency injection and Polly resilience policy extensions.

As of **v3.0**, the library adopts a consumer-provided HttpClient approach. Consumers configure their own HttpClient with authentication, resilience policies, and other settings.

## What Changed

- **Removed**: `AddPlanningCenterApi()` DI registration methods
- **Removed**: `AddStandardResiliencePolicies()` and other Polly extensions
- **Removed**: `PlanningCenterApiOptions` configuration class

## New Approach

Consumers now control HttpClient configuration directly:

```csharp
using Crews.PlanningCenter.Api.Authentication;

// Configure HttpClient with authentication helpers
builder.Services.AddHttpClient("PlanningCenterApi", client =>
{
    client.ConfigureForPlanningCenter()
          .AddPlanningCenterAuth(new PlanningCenterPersonalAccessToken
          {
              AppId = builder.Configuration["PlanningCenter:AppId"]!,
              Secret = builder.Configuration["PlanningCenter:Secret"]!
          });
})
.AddStandardResilienceHandler(); // Optional: .NET 8+ built-in resilience
```

## Authentication Helpers

For authentication helpers, see:
- [HttpClientAuthenticationExtensions.cs](../Authentication/HttpClientAuthenticationExtensions.cs)

## Documentation

- [Main README.md](../../README.md) - Complete usage examples
- [MIGRATION_V3.md](../../MIGRATION_V3.md) - Migration guide from v2.x to v3.0
