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

### Login and Logout Endpoints

Map the standard ASP.NET Core authentication endpoints in `Program.cs`:

```csharp
app.MapGet("/login", () => Results.Challenge(
    new AuthenticationProperties { RedirectUri = "/" },
    [PlanningCenterAuthenticationDefaults.AuthenticationScheme]))
    .AllowAnonymous();

app.MapGet("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/");
}).RequireAuthorization();
```

The `/login` route triggers the OIDC challenge, redirecting the user to Planning Center's authorization page. After a successful sign-in, Planning Center redirects back to `/signin-oidc` (handled automatically by the OIDC middleware), which then redirects to `RedirectUri`. The `/logout` route clears the local cookie session.

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
