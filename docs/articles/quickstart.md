# Quick Start

This guide will help you get started with the Planning Center API client library.

## Basic Setup

```csharp
using Crews.PlanningCenter.Api;
using Crews.PlanningCenter.Api.Authentication;
using System.Net.Http.Headers;

// Configure an HttpClient with authentication
PlanningCenterPersonalAccessToken token = new()
{
    AppId = "your-app-id",
    Secret = "your-secret"
};

var httpClient = new HttpClient
{
    BaseAddress = new Uri(PlanningCenterAuthenticationDefaults.BaseUrl)
};
httpClient.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));
httpClient.DefaultRequestHeaders.Authorization = token; // implicit conversion

// Use the root PeopleClient to access the latest API version
var org = new PeopleClient(httpClient).Latest;

// Navigate to a specific person and fetch it
var response = await org.People.WithId("123").GetAsync();
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
        AppId = builder.Configuration["PlanningCenter:AppId"]!,
        Secret = builder.Configuration["PlanningCenter:Secret"]!
    };
    client.BaseAddress = new Uri(PlanningCenterAuthenticationDefaults.BaseUrl);
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Authorization = token;
})
.AddStandardResilienceHandler(); // Optional: add .NET 8+ built-in resilience

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
        var org = new PeopleClient(httpClient).Latest;
        var response = await org.People.WithId(personId).GetAsync();
        return response.Data;
    }
}
```

## Next Steps

- Learn about [Authentication](authentication.md) options
- Explore [usage patterns](usage.md) and examples
- Understand [working with resources](resources.md)
