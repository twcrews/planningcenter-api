# Authentication

The Planning Center API client library supports two authentication methods.

## Personal Access Token

Best for server-to-server integrations and development. Configure your `HttpClient` manually:

```csharp
using Crews.PlanningCenter.Api.Authentication;
using System.Net.Http.Headers;

PlanningCenterPersonalAccessToken token = new()
{
    AppId = "app-id",
    Secret = "secret"
};

var httpClient = new HttpClient
{
    BaseAddress = new Uri(PlanningCenterAuthenticationDefaults.BaseUrl)
};
httpClient.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));
httpClient.DefaultRequestHeaders.Authorization = token; // implicit conversion to Basic auth header
```

`PlanningCenterPersonalAccessToken` implicitly converts to `AuthenticationHeaderValue` using HTTP Basic authentication (Base64-encoded `AppId:Secret`).

## OIDC Authentication (Recommended for Web Apps)

For ASP.NET Core web applications, use the built-in OIDC extension:

```csharp
// Configure authentication in Program.cs
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = PlanningCenterAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddPlanningCenterAuthentication();  // Reads from appsettings.json
```

### Configuration (appsettings.json)

```json
{
  "Authentication": {
    "PlanningCenter": {
      "Authority": "https://api.planningcenteronline.com",
      "ClientId": "your-client-id",
      "ClientSecret": "your-client-secret",
      "Scopes": ["openid", "people"]
    }
  }
}
```

`ClientId` and `ClientSecret` are required. `Authority` defaults to Planning Center's base URL, and `Scopes` defaults to `["openid", "people"]` if not specified.

### Manual Configuration

Configure OIDC options directly instead of using appsettings.json:

```csharp
builder.Services
    .AddAuthentication()
    .AddPlanningCenterAuthentication(options =>
    {
        options.ClientId = "your-client-id";
        options.ClientSecret = "your-client-secret";
    });
```

## Registering API Clients for Dependency Injection

After configuring authentication, call `AddPlanningCenterApi()` to register all product clients (`PeopleClient`, `CalendarClient`, etc.) with the DI container. When used alongside `AddPlanningCenterAuthentication()`, it automatically forwards the OIDC bearer token from the current HTTP context:

```csharp
builder.Services
    .AddAuthentication(...)
    .AddCookie()
    .AddPlanningCenterAuthentication();

builder.Services.AddPlanningCenterApi();
```

Product clients can then be injected directly into your services and controllers. See the [Usage](usage.md) guide for examples.

## Security Best Practices

- Never commit credentials to source control
- Use User Secrets for development
- Use environment variables or secure vaults in production
- Rotate credentials regularly
- Use the principle of least privilege for API scopes
