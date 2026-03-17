# .NET Planning Center API Library

[![Build](https://github.com/twcrews/planningcenter-api/actions/workflows/ci.yml/badge.svg)](https://github.com/twcrews/planningcenter-api/actions/workflows/ci.yml)
[![Coverage](https://codecov.io/gh/twcrews/planningcenter-api/branch/master/graph/badge.svg)](https://codecov.io/gh/twcrews/planningcenter-api)

A strongly-typed .NET client library for the [Planning Center API](https://developer.planning.center/docs/), featuring a fluent syntax and comprehensive authentication support.

## Installation

```sh
dotnet add package Crews.PlanningCenter.Api
```

## Quick Start

```csharp
PlanningCenterPersonalAccessToken token = new()
{
    AppId = "your-app-id",
    Secret = "your-secret"
};

var httpClient = new HttpClient
{
    BaseAddress = new Uri(PlanningCenterAuthenticationDefaults.BaseUrl)
};
httpClient.DefaultRequestHeaders.Authorization = token;

var client = new PeopleClient(httpClient).Latest;

var response = await client
    .People
    .WithId("123")
    .GetAsync();

Console.WriteLine($"Person: {response.Data?.Attributes.Name}");
```

## Documentation

Full documentation — including authentication, usage examples, and API reference — is available at **[pcapi.crews.dev](https://pcapi.crews.dev)**.

***

> S.D.G.
