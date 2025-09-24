# .NET Planning Center API Library

A client library for the Planning Center API built on the 
[JSON:API Framework](https://github.com/scott-mcdonald/JsonApiFramework).

- [Installation](#installation)
- [Configuration](#configuration)
  - [OAuth Options](#oauth-options)
  - [API Options](#api-options)
- [Setup](#setup)
  - [ASP.NET Core Application Builder](#aspnet-core-application-builder)
  - [Standalone](#standalone)
- [Examples](#examples)
  - [Fluent API Example](#fluent-api-example)
  - [Querying Example](#querying-example)
  - [Pagination Example](#pagination-example)
  - [Mutation Example](#mutation-example)

## Installation

`Crews.PlanningCenter.Api` is available on [NuGet](https://www.nuget.org/packages/Crews.PlanningCenter.Api):

```sh
dotnet add package Crews.PlanningCenter.Api
```

## Configuration

This library uses the [options pattern](https://learn.microsoft.com/en-us/dotnet/core/extensions/options). By default, the following configuration sections are defined:

- `Authentication:PlanningCenter`: mapped to the `PlanningCenterOAuthOptions` class. Used for defining [OAuth](https://developer.planning.center/docs/#/overview/authentication%23oauth-2-0) credentials.
- `PlanningCenterApi`: mapped to the `PlanningCenterApiOptions` class. Used for defining all other options for the library.

### OAuth Options

The following are valid options for OAuth authentication:

| Option Name | Type | Description |
| ----------- | ---- | ----------- |
| `ClientId` | `string` | The OAuth client ID. |
| `ClientSecret` | `string` | The OAuth client secret. |

> [!TIP]
> This class also accepts all options defined in the [`OAuthOptions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.oauthoptions) class.

`appsettings.json` example:

```json
{
  "Authentication": {
    "PlanningCenter": {
      "ClientId": "my-client-id",
      "ClientSecret": "my-client-secret"
    }
  }
}
```

Manual configuration example:

```cs
builder.Services.AddAuthentication(options =>
{
  options.DefaultScheme = "Cookies";
  options.DefaultChallengeScheme = PlanningCenterOAuthDefaults.AuthenticationScheme;
})
.AddCookie("Cookies")
.AddPlanningCenterOAuth(options =>
{
  // The strings here are hardcoded for example.
  // These credentials should never be stored in your code.
  options.ClientId = "my-client-id";
  options.ClientSecret = "my-secret";
});
```

### API Options

| Option Name | Type | Description | Default |
| ----------- | ---- | ----------- | ------- |
| `ApiBaseAddress` | `Uri` | The base URL of the Planning Center API. | `https://api.planningcenteronline.com` |
| `HttpClientName` | `string` | The name of an optional [named](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests#named-clients) `HttpClient` to use in the service.<br><br>**NOTE**: This client's `BaseAddress` property will be ignored in favor of the `ApiBaseAddress` option. | N/A |
| `UserAgent` | `string` | The user agent string to send on API requests. This is [required](https://developer.planning.center/docs/#/overview/authentication%23specifying-user-agent) by Planning Center. | `Generic .NET Planning Center API Client` |
| `PersonalAccessToken` | `PlanningCenterPersonalAccessToken` | An optional Planning Center [personal access token](https://developer.planning.center/docs/#/overview/authentication%23personal-access-token). | N/A |

`appsettings.json` example:

```json
{
  "PlanningCenterApi": {
    "ApiBaseAddress": "http://some-address.com",
    "HttpClientName": "my-custom-client",
    "UserAgent": "Some custom user agent string",
    "PersonalAccessToken": {
      "AppId": "my-app-id",
      "Secret": "my-secret"
    }
  }
}
```

Manual configuration example:

```cs
builder.Services.AddPlanningCenterApi(options =>
{
  options.ApiBaseAddress = new("http://some-address.com");
  options.HttpClientName = "my-custom-client";
  options.UserAgent = "Some custom user agent string";

  // The strings here are hardcoded for example.
  // These credentials should never be stored in your code.
  options.PersonalAccessToken = new()
  {
    AppId = "my-app-id",
    Secret = "my-secret"
  };
});
```

## Setup

This library can be used with application builders (dependency injection), or as a standalone client.

### ASP.NET Core Application Builder

First, add OAuth authentication:

```cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
  options.DefaultScheme = "Cookies";
  options.DefaultChallengeScheme = PlanningCenterOAuthDefaults.AuthenticationScheme;
})
.AddCookie("Cookies")
.AddPlanningCenterOAuth(options =>
{
  // Add scopes for the APIs you need access to
  // By default, the "People" scope is always included
  options.Scope.Clear();
  options.Scope.Add(PlanningCenterOAuthScope.People);
  options.Scope.Add(PlanningCenterOAuthScope.Calendar);
  options.Scope.Add(PlanningCenterOAuthScope.CheckIns);
});
```

Next, add the Planning Center API service:

```cs
builder.Services.AddPlanningCenterApi();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
```

Finally, add authentication endpoints to your app. The OAuth middleware will automatically handle token management and refreshing.

```cs
app.MapGet("/login", (HttpContext context, string? returnUrl = null) =>
{
  var properties = new AuthenticationProperties
  {
    RedirectUri = returnUrl ?? "/"
  };
  return Results.Challenge(properties, [PlanningCenterOAuthDefaults.AuthenticationScheme]);
});

app.MapPost("/logout", async (HttpContext context) =>
{
  await context.SignOutAsync("Cookies");
  return Results.Redirect("/");
});
```

Now you can inject `IPlanningCenterApiService` into your controllers or services.

```cs
[Authorize]
public class MyController(IPlanningCenterApiService _planningCenterApi) : Controller
{
  public async Task<IActionResult> Profile()
  {
    // API calls automatically use the authenticated user's OAuth token, or your personal access token
    var currentUser = await _planningCenterApi.People.LatestVersion
      .GetRelated<PersonResource>("me")
      .GetAsync();

    return View(currentUser.Data);
  }
}
```

### Standalone

Start by creating an `HttpClient` instance:

```cs
HttpClient client = new();
client.SafelySetBaseAddress(new("https://api.planningcenteronline.com"));

// This example uses a personal access token.
// If you want to use OAuth in a standalone client, you're on your own.
PlanningCenterPersonalAccessToken token = new()
{
  AppID = "yourAppIdHere",
  Secret = "superSecretPasswordHere"
};
client.DefaultRequestHeaders.Authorization = token;
```

Use this `HttpClient` to create a new API client of your choice, and you're off to the races!

```cs
CalendarClient calendar = new(client);

var myEvent = await calendar.LatestVersion.Events.WithID("123").GetAsync();
Console.WriteLine($"My event is called {myEvent.Data.Name}!");
```

## Examples

### Fluent API Example

You can chain API resource calls to navigate the API:

```cs
var myEvent = await calendar.LatestVersion
  .Events
  .WithID("123")
  .Owner
  .EventResourceRequests
  .WithID("456")
  .ResourceBookings
  .WithID("789")
  .EventInstance
  .Event
  .GetAsync();
```

### Querying Example

You can easily _include_ related resources, or _sort_ and _query_ collections:

```cs
var myAttachments = await calendar.LatestVersion.Attachments
  .Include(AttachmentIncludable.Event)
  .OrderBy(AttachmentOrderable.FileSize, Order.Descending)
  .Query((AttachmentQueryable.Name, "myAttachment"), (AttachmentQueryable.Description, "The best attachment."))
  .GetAllAsync();

// Reading included resources must be done manually.
var includedResources = myAttachments.JsonApiDocument.GetIncludedResources();
```

### Pagination Example

You can specify a count and an offset for collections of resources:

```cs
var myAttachments = await calendar.LatestVersion.Attachments.GetAllAsync();

Console.WriteLine(myAttachments.Metadata.TotalCount);  // Get total count of items available on the API
Console.WriteLine(myAttachments.Metadata.Next.Offset);  // Get item offset for next page of items

// Get only first five items
myAttachments = await calendar.LatestVersion.Attachments.GetAllAsync(count: 5);

// Get ten items, offset by five items
myAttachments = await calendar.LatestVersion.Attachments.GetAllAsync(count: 10, offset: 5);
```

### Mutation Example

You can also `POST`, `PATCH`, and `DELETE` resources with these options:

```cs
var myEventConnection = calendar.LatestVersion
  .Events
  .WithID("123")
  .EventConnections
  .WithID("456");

EventConnection newConnection = new()
{
  ConectedToId = "123",
  ConnectedToName = "Test"
};

// POST
var postResult = await myEventConnection.PostAsync(newConnection);

// PATCH
var patchResult = await myEventConnection.PatchAsync(newConnection);

// DELETE
await myEventConnection.DeleteAsync();
```

***

> S.D.G.
