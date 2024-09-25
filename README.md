# Planning Center API Library

A client library for the Planning Center API built on the 
[JSON:API Framework](https://github.com/scott-mcdonald/JsonApiFramework).

> [!NOTE]
> This is a "scaffolding library" that consists mostly of abstract members and interfaces. It is intended to be 
> implemented as a dependency in the development of higher level Planning Center API clients.

## Installation

`Crews.PlanningCenter.Api` is available on [NuGet](https://www.nuget.org/packages/Crews.PlanningCenter.Api):

```sh
dotnet add package Crews.PlanningCenter.Api
```

## Basic Usage

1. Create a document context derived from `PlanningCenterDocumentContext`:

```cs
class MyContext : PlanningCenterDocumentContext
{
	// Add any conventions or other context settings supported by the JSON:API Framework
}
```

2. Create a resource POCO type:

```cs
class MyResource
{
	public string? FavoriteColor { get; set; }
	public int FavoriteNumber { get; set; }
}
```

3. Create a fetchable resource wrapper:

```cs
class MyFetchableResource : PlanningCenterSingletonFetchableResource<MyResource, MyFetchableResource, MyContext>
{
	private static readonly Uri _resourceUri = new("https://api-url.com/path/to/resource");

	// The following is for example only; do not use HttpClient like this.
	private static readonly HttpClient _client = new();

	public MyFetchableResource() : base(_resourceUri, _client) { }

	// Optionally implement POST and PATCH members, if your resource supports them.
	public new Task<MyResource?> PostAsync(MyResource resource) => base.PostAsync(resource);
	public new Task<MyResource?> PatchAsync(MyResource resource) => base.PatchAsync(resource);
}
```

4. Fetch your resource!

```cs
MyFetchableResource fetchableResource = new();
MyResource resource = await fetchableResource.GetAsync();
```