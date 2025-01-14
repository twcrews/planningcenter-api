# Planning Center API Library

A client library for the Planning Center API built on the 
[JSON:API Framework](https://github.com/scott-mcdonald/JsonApiFramework).

## Installation

> [!TIP]
> Using dependency injection? Check out the [Crews.PlanningCenter.DependencyInjection](https://www.nuget.org/packages/Crews.PlanningCenter.DependencyInjection/) package instead!

`Crews.PlanningCenter.Api` is available on [NuGet](https://www.nuget.org/packages/Crews.PlanningCenter.Api):

```sh
dotnet add package Crews.PlanningCenter.Api
```

## Usage

Start by creating an `HttpClient` instance with a Planning Center API base address:

```cs
using Crews.Extensions.Http;
using Crews.PlanningCenter.Api.Clients;

HttpClient client = new();
client.SafelySetBaseAddress(new("https://api.planningcenteronline.com"));
```

Use this `HttpClient` to create a new API client of your choice, and you're off to the races!

```cs
CalendarClient calendar = new(client);

var myEvent = await calendar.LatestVersion.Events.WithID("123").GetAsync();
Console.WriteLine($"My event is called {myEvent.Data.Name}!");
```

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