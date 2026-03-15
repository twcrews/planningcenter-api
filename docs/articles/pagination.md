# Pagination

How to work with paginated responses in the Planning Center API client library.

## Overview

Collection endpoints in the Planning Center API are paginated. Each response includes a page of results along with metadata you can use to determine the total record count and fetch subsequent pages.

## Page Size and Offset

Use `.PerPage()` and `.Offset()` to control which page of results is returned:

```csharp
var org = new PeopleClient(httpClient).Latest;

// Fetch the second page of 25 people
var response = await org.People
    .PerPage(25)
    .Offset(25)
    .GetAsync();
```

- `PerPage(n)` sets the `per_page` query parameter (max 100)
- `Offset(n)` sets the `offset` query parameter (zero-based)

## Reading Pagination Metadata

Every collection response exposes pagination metadata via `ResponseBody.Meta`. The Planning Center API includes the following fields:

| Field | Type | Description |
|---|---|---|
| `total_count` | `int` | Total number of records matching the query |
| `count` | `int` | Number of records in the current page |
| `next.offset` | `int?` | Offset to pass for the next page (absent if on the last page) |
| `prev.offset` | `int?` | Offset to pass for the previous page (absent if on the first page) |

```csharp
var response = await org.People.PerPage(25).GetAsync();

var meta = response.ResponseBody?.Meta;
var totalCount = meta?["total_count"]?.GetValue<int>();
var count = meta?["count"]?.GetValue<int>();
var nextOffset = meta?["next"]?["offset"]?.GetValue<int>();

Console.WriteLine($"Showing {count} of {totalCount} people");

if (nextOffset.HasValue)
    Console.WriteLine($"Next page offset: {nextOffset}");
```

## Iterating All Pages

To fetch all records across multiple pages, loop until `next` is absent from the metadata:

```csharp
var org = new PeopleClient(httpClient).Latest;
var allPeople = new List<PersonResource>();
int offset = 0;
const int pageSize = 100;

while (true)
{
    var response = await org.People
        .PerPage(pageSize)
        .Offset(offset)
        .GetAsync();

    if (response.Data is not null)
        allPeople.AddRange(response.Data);

    var nextOffset = response.ResponseBody?.Meta?["next"]?["offset"]?.GetValue<int>();
    if (nextOffset is null)
        break;

    offset = nextOffset.Value;
}
```

## Checking Available Filters and Ordering

The `meta` object also describes what query options the endpoint supports:

```csharp
var meta = response.ResponseBody?.Meta;

// Fields that support filtering
var canFilter = meta?["can_filter"]?.AsArray()
    .Select(f => f?.GetValue<string>())
    .ToList();

// Fields that support sorting
var canOrderBy = meta?["can_order_by"]?.AsArray()
    .Select(f => f?.GetValue<string>())
    .ToList();
```

These lists mirror what is documented in the [Planning Center API documentation](https://developer.planning.center/docs/).
