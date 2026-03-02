# .NET Planning Center API Library

A strongly-typed .NET client library for the [Planning Center API](https://developer.planning.center/docs/), featuring automatic code generation and comprehensive authentication support.

## Installation

```sh
dotnet add package Crews.PlanningCenter.Api
```

## Quick Start

```csharp
using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.People.V2025_11_10;
using System.Net.Http.Headers;

PlanningCenterPersonalAccessToken token = new()
{
    AppId = "your-app-id",
    Secret = "your-secret"
};

var httpClient = new HttpClient
{
    BaseAddress = new Uri(PlanningCenterAuthenticationDefaults.BaseUrl)
};
httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
httpClient.DefaultRequestHeaders.Authorization = token;

var client = new PersonClient(httpClient,
    new Uri("/people/v2/people/123", UriKind.Relative));

var response = await client.GetAsync();
Console.WriteLine($"Person: {response.Data?.Attributes.Name}");
```

## Documentation

Full documentation — including authentication, usage examples, and API reference — is available at **[twcrews.github.io/planningcenter-api](https://twcrews.github.io/planningcenter-api/)**.

***

> S.D.G.
