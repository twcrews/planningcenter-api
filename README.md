# .NET Planning Center API Library

A client library for the Planning Center API built on the 
[JSON:API Framework](https://github.com/scott-mcdonald/JsonApiFramework).

- [Installation](#installation)
- [Configuration](#configuration)
  - [Basic Setup](#basic-setup)
  - [Advanced Configuration](#advanced-configuration)
- [Usage](#usage)
  - [Standalone](#standalone)
- [Examples](#examples)
  - [Fluent API Example](#fluent-api-example)
  - [Querying Example](#querying-example)
  - [Pagination Example](#pagination-example)
  - [Mutation Example](#mutation-example)
  - [Custom Resource Example](#custom-resource-example)

## Installation

`Crews.PlanningCenter.Api` is available on [NuGet](https://www.nuget.org/packages/Crews.PlanningCenter.Api):

```sh
dotnet add package Crews.PlanningCenter.Api
```

## Configuration

This library uses the [options pattern](https://learn.microsoft.com/en-us/dotnet/core/extensions/options). By default, the following configuration sections are defined:

- `Authentication:PlanningCenter`: Mapped to the [`OpenIdConnectOptions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authentication.openidconnect.openidconnectoptions) class. Used for defining [OIDC](https://developer.planning.center/docs/#/overview/authentication%23oidc) options.
- `PlanningCenterClient`: mapped to the `PlanningCenterClientOptions` class. Used for defining all other options for the library.

### Basic Setup

Add the following to `appsettings.json`:

```json
{
  "Authentication": {
    "PlanningCenter": {
      "ClientId": "your-client-id",
      "ClientSecret": "your-client-secret",
      "Scope": [
        "calendar",
        "people",
        "registrations"
      ]
    }
  }
}
```

Then, add authentication services to `Program.cs`. This example uses cookies:

```cs
builder.Services.AddAuthentication(options =>
{
  options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = PlanningCenterOAuthDefaults.AuthenticationScheme;
})
.AddCookie()
.AddPlanningCenter();

// Don't forget to register the client itself!
builder.Services.AddPlanningCenterClient();
```

### Advanced Configuration

You can configure the `PlanningCenterClient` configuration section with the following options:

| Option Name | Type | Description | Default |
| ----------- | ---- | ----------- | ------- |
| `ApiBaseAddress` | `Uri` | The base URL of the Planning Center API. | `https://api.planningcenteronline.com` |
| `HttpClientName` | `string` | The [name](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests#named-clients) of the internal `HttpClient` instance. | `__defaultPlanningCenterClient` |
| `UserAgent` | `string` | The user agent string to send on API requests as [required](https://developer.planning.center/docs/#/overview/authentication%23specifying-user-agent) by Planning Center. | `Generic .NET Planning Center API Client` |
| `PersonalAccessToken` | `PlanningCenterPersonalAccessToken` | An optional Planning Center [personal access token](https://developer.planning.center/docs/#/overview/authentication%23personal-access-token). | N/A |

`appsettings.json` example:

```json
{
  "PlanningCenterClient": {
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
builder.Services.AddPlanningCenterClient(options =>
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

## Usage

You can inject `IPlanningCenterClient` into your controllers or services.

```cs
[Authorize]
public class MyController(IPlanningCenterClient _planningCenterClient) : Controller
{
  public async Task<IActionResult> Profile()
  {
    // API calls automatically use the authenticated user's OAuth token, or your personal access token
    var currentUser = await _planningCenterClient.People.LatestVersion
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

### Custom Resource Example

You can define your own resource types by inheriting from either `PlanningCenterSingletonFetchableResource` or `PlanningCenterPaginatedFetchableResource`.

This provides forward compatibility by allowing you to define and fetch any resources not yet implemented in this library.

#### Model Creation

First, create a model class:

```cs
[JsonApiName("my_custom_resource")]
public record MyCustomModel
{
  [JsonApiName("my_first_property")]
  public string? MyFirstProperty { get; init; }

  [JsonProperty("my_second_property")]
  public int? MySecondProperty { get; init; }
}
```

Next, you can create your resource class.

#### Singleton Resource

```cs
public class MyCustomResource 
  : PlanningCenterSingletonFetchableResource<MyCustomModel, MyCustomResource, PeopleDocumentContext>,
  IIncludable<MyCustomResource, MyCustomResourceIncludable>
{
  // In this example, your resource has child resources of type "people"
  public PeopleResourceCollection People => GetRelated<PeopleResourceCollection>("people");

  public MyCustomSingletonResource(Uri uri, HttpClient client) : base(uri, client) { }

  // In this example, your resource has certain includable resources
  public MyCustomResource Include(params MyCustomResourceIncludable[] included) => base.Include(included);
}
```

#### Paginated Resource

> [!IMPORTANT]
> Creating a paginated resource type requires the existence of a singleton resource type for the same model.

```cs
public class MyCustomResourceCollection 
  : PlanningCenterPaginatedFetchableResource<MyCustomModel, MyCustomResourceCollection, MyCustomResource, PeopleDocumentContext>,
  IIncludable<MyCustomResourceCollection, MyCustomResourceIncludable>,
  IOrderable<MyCustomResourceCollection, MyCustomResourceOrderable>,
  IFilterable<MyCustomResourceCollection, MyCustomResourceQueryable>
{
  public MyCustomResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  // In this example, your resource has certain includable resources
  public MyCustomResourceCollection Include(params MyCustomResourceIncludable[] included) => base.Include(included);

  // In this example, your resource has certain orderable properties
  public MyCustomResourceCollection OrderBy(MyCustomResourceOrderable orderable, Order order = Order.Ascending) => base.OrderBy(orderable, order);

  // In this example, your resource has certain queryable properties
  public MyCustomResourceCollection Query(params (MyCustomResourceQueryable, string)[] queries) => base.Query(queries);
}
```

#### Accessing your custom resources

Simply call the `GetRelated<T>()` method on any singleton resource to access your custom resource:

```cs
// Singleton
var currentUser = await planningCenterApi.People.LatestVersion
  .GetRelated<MyCustomResource>("my_custom_resource");

// Paginated
var customResources = await planningCenterApi.People.LatestVersion
  .GetRelated<MyCustomResourceCollection>("my_custom_resources");
```

***

> S.D.G.
