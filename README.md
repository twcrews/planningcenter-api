# .NET Planning Center API Library

A client library for the Planning Center API built on the 
[JSON:API Framework](https://github.com/scott-mcdonald/JsonApiFramework).

- [Installation](#installation)
- [Setup with Dependency Injection](#setup-with-dependency-injection)
	- [Configuration Provider (Recommended)](#configuration-provider-recommended)
	- [Hardcoded Configuration](#hardcoded-configuration)
- [Basic Setup (Without Dependency Injection)](#basic-setup-without-dependency-injection)
- [Usage Examples](#usage-examples)
	- [Fluent API Example](#fluent-api-example)
	- [Querying Example](#querying-example)
	- [Pagination Example](#pagination-example)
	- [Mutation Example](#mutation-example)

## Installation

`Crews.PlanningCenter.Api` is available on [NuGet](https://www.nuget.org/packages/Crews.PlanningCenter.Api):

```sh
dotnet add package Crews.PlanningCenter.Api
```

## Setup with Dependency Injection

How you set up dependency injection depends on how you want to configure the service.

This library uses the [options pattern](https://learn.microsoft.com/en-us/dotnet/core/extensions/options). Options are defined in the `PlanningCenterApiOptions` class. 

The following are valid options for configuring the service:

| Option Name           | Type                                | Required | Description                                                                                                                                                                                                                                                      | Default                                |
| --------------------- | ----------------------------------- | -------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------- |
| `ApiBaseAddress`      | `Uri`                               | No       | The base URL of the Planning Center API.                                                                                                                                                                                                                         | `https://api.planningcenteronline.com` |
| `PersonalAccessToken` | `PlanningCenterPersonalAccessToken` | Yes      | The access token used to authenticate with the API.                                                                                                                                                                                                              | N/A                                    |
| `HttpClientName`      | `string`                            | No       | The name of a [named](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests#named-clients) `HttpClient` to use in the service.<br><br>**NOTE**: This client's `BaseAddress` property will be ignored in favor of the `ApiBaseAddress` option. | N/A                                    |

### Configuration Provider (Recommended)

You can automatically configure the service with the provider of your choice. Here's an example using `appsettings.json`:

```json
{
	"PlanningCenterApi": {
		"ApiBaseAddress": "https://api.planningcenteronline.com",
		"PersonalAccessToken": {
			"AppID": "yourAppId",
			"Secret": "yourSecret"
		},
		"HttpClientName": "myCustomClient"
	}
}
```

Then, in `Program.cs` or `Startup.cs`:

```cs
using Crews.PlanningCenter.Api.DependencyInjection;

// ...

builder.Services.AddPlanningCenterApi();
```

### Hardcoded Configuration

You can also configure the service using a lambda expression during service registration:

```cs
using Crews.PlanningCenter.Api.DependencyInjection;

// ...

builder.Services.AddPlanningCenterApi(options =>
{
	options.ApiBaseAddress = new("https://api.planningcenteronline.com");
	options.PersonalAccessToken = new()
	{
		AppID = "yourAppId",
		Secret = "yourSecret"
	};
	options.HttpClientName = "myCustomClient";
});
```

## Basic Setup (Without Dependency Injection)

Start by creating an `HttpClient` instance:

```cs
using Crews.Extensions.Http;
using Crews.PlanningCenter.Api.Clients;
using Crews.PlanningCenter.Api.Models;

HttpClient client = new();
client.SafelySetBaseAddress(new("https://api.planningcenteronline.com"));

// Don't forget to add your access token!
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

## Usage Examples

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

var postResult = await myEventConnection.PostAsync(newConnection);   // POST
var patchResult = await myEventConnection.PatchAsync(newConnection); // PATCH
await myEventConnection.DeleteAsync();                               // DELETE
```

***

> S.D.G.