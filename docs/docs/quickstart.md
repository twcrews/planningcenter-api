# Quick Start

This guide will help you get started with the Planning Center API client library.

## Basic Setup

```csharp
using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.People.V2025_11_10;

// Create and configure HttpClient with authentication
var httpClient = new HttpClient()
    .ConfigureForPlanningCenter()
    .AddPlanningCenterAuth(new PlanningCenterPersonalAccessToken
    {
        AppId = "your-app-id",
        Secret = "your-secret"
    });

// Create a resource client
var client = new PersonClient(httpClient,
    new Uri("/people/v2/people/123", UriKind.Relative));

// Make an API call
var response = await client.GetAsync();
Console.WriteLine($"Person: {response.Data?.Attributes.Name}");
```

## ASP.NET Core Setup

For ASP.NET Core applications, use dependency injection:

```csharp
// Program.cs
builder.Services.AddHttpClient("PlanningCenterApi", client =>
{
    client.ConfigureForPlanningCenter()
          .AddPlanningCenterAuth(new PlanningCenterPersonalAccessToken
          {
              AppId = builder.Configuration["PlanningCenter:AppId"]!,
              Secret = builder.Configuration["PlanningCenter:Secret"]!
          });
});

// In your service
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

## Next Steps

- Learn about [Authentication](authentication.md) options
- Explore [usage patterns](usage.md) and examples
- Understand [working with resources](resources.md)
