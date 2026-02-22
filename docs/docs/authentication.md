# Authentication

The Planning Center API client library supports multiple authentication methods.

## Personal Access Token

Best for server-to-server integrations and development.

```csharp
var httpClient = new HttpClient()
    .ConfigureForPlanningCenter()
    .AddPlanningCenterAuth(new PlanningCenterPersonalAccessToken
    {
        AppId = "app-id",
        Secret = "secret"
    });
```

## OAuth Bearer Token

When you already have an access token:

```csharp
var httpClient = new HttpClient()
    .ConfigureForPlanningCenter()
    .AddPlanningCenterAuth("access-token");
```

## OIDC Authentication (Recommended for Web Apps)

For ASP.NET Core web applications:

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

// Configure HttpClient
builder.Services.AddHttpClient("PlanningCenterApi", client =>
{
    client.ConfigureForPlanningCenter();
});
```

### Configuration (appsettings.json)

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

## Security Best Practices

- Never commit credentials to source control
- Use User Secrets for development
- Use environment variables or secure vaults in production
- Rotate credentials regularly
- Use the principle of least privilege for API scopes
