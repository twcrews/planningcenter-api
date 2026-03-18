# Quick Start

In this guide, you'll build a minimal application for testing out the API client.

> [!IMPORTANT]
> **Never** put credentials or other sensitive info in your code. These examples are for demonstration only.

## Basic Setup

```csharp
using Crews.PlanningCenter.Api;
using Crews.PlanningCenter.Api.Authentication;
using System.Net.Http.Headers;

// Configure an HttpClient with authentication
HttpClient httpClient = new()
{
    BaseAddress = new Uri(PlanningCenterAuthenticationDefaults.BaseUrl)
};

PlanningCenterPersonalAccessToken token = new()
{
    AppId = "your-app-id",
    Secret = "your-secret"
};
httpClient.DefaultRequestHeaders.Authorization = token;

// Use the PeopleClient to access the latest API version
PeopleClient peopleClient = new(httpClient).Latest;

// Navigate to a specific person and fetch it
var response = await peopleClient
    .People
    .WithId("123")
    .GetAsync();

Console.WriteLine($"Person: {response.Data?.Attributes?.Name}");
```

## ASP.NET Core Setup

For ASP.NET Core applications, use dependency injection:

```csharp
// Program.cs
using Crews.PlanningCenter.Api.Authentication;
using System.Net.Http.Headers;

builder.Services.AddHttpClient("PlanningCenterApi", client =>
{
    PlanningCenterPersonalAccessToken token = new()
    {
        AppId = builder.Configuration["Authentication:PlanningCenter:AppId"],
        Secret = builder.Configuration["Authentication:PlanningCenter:Secret"]
    };
    client.BaseAddress = new Uri(PlanningCenterAuthenticationDefaults.BaseUrl);
    client.DefaultRequestHeaders.Authorization = token;
});

// In your service
public class PeopleService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PeopleService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<PersonResource?> GetPersonAsync(string personId)
    {
        var httpClient = _httpClientFactory.CreateClient("PlanningCenterApi");
        var peopleClient = new PeopleClient(httpClient).Latest;
        
        var response = await peopleClient
            .People
            .WithId(personId)
            .GetAsync();

        return response.Data;
    }
}
```

## OIDC Setup

For web applications that sign users in via Planning Center, use the built-in OIDC integration:

```csharp
// Program.cs
using Microsoft.AspNetCore.Authentication.Cookies;

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = PlanningCenterAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddPlanningCenterAuthentication();

builder.Services.AddPlanningCenterApi();
```

Add your credentials to `appsettings.json` (or user secrets):

```json
{
  "PlanningCenter": {
    "ClientId": "your-client-id",
    "ClientSecret": "your-client-secret"
  }
}
```

Then inject any product client directly into your services — the bearer token from the signed-in user is forwarded automatically:

```csharp
public class PeopleService(PeopleClient peopleClient)
{
    public async Task<PersonResource?> GetPersonAsync(string personId)
        => (await peopleClient.Latest.People.WithId(personId).GetAsync()).Data;
}
```

## Next Steps

- Learn about [Authentication](authentication.md) options
- Explore [usage patterns](usage.md) and examples
- Understand [working with resources](resources.md)
