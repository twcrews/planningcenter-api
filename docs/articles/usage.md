# Using the API Client

Common patterns and examples for using the Planning Center API client library.

## Fluent Syntax

The library uses a fluent traversal model beginning at the product level. Each product has a root client (e.g., `PeopleClient`) that provides access to versions of that product's API. From there, you navigate through related resource clients:

```csharp
// Root client → versioned client → resource collection client → specific resource client
var peopleClient = new PeopleClient(httpClient).V2025_11_10;

// Access a collection
var people = peopleClient.People;

// Access a specific resource by ID
var personClient = peopleClient.People.WithId("123");

// Access nested resources
var addresses = peopleClient.People.WithId("123").Addresses;
```

The `.Latest` property automatically sets the `X-PCO-API-Version` header to the latest supported version. You can also use a specific version property (e.g., `.V2025_11_10`).

## Client Lifetime Management

### ASP.NET Core with OIDC (Recommended)

When using OIDC authentication via `AddPlanningCenterAuthentication()`, call `AddPlanningCenterApi()` to register all product clients for dependency injection. It automatically configures an `HttpClient` that forwards the OIDC bearer token from the current HTTP context:

```csharp
// Program.cs
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = PlanningCenterAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddPlanningCenterAuthentication();

builder.Services.AddPlanningCenterApi();

// In your service — inject any product client directly
public class PeopleService(PeopleClient peopleClient)
{
    public async Task<PersonResource?> GetPersonAsync(string personId)
    {
        var response = await peopleClient.Latest.People.WithId(personId).GetAsync();
        return response.Data;
    }
}
```

### ASP.NET Core with a Named HttpClient

If you manage your own `HttpClient` (e.g., using a Personal Access Token), register it by name and pass that name to `AddPlanningCenterApi()`:

```csharp
// Program.cs
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
.AddStandardResilienceHandler();

builder.Services.AddPlanningCenterApi("PlanningCenterApi");

// In your service — inject any product client directly
public class PeopleService(PeopleClient peopleClient)
{
    public async Task<PersonResource?> GetPersonAsync(string personId)
    {
        var response = await peopleClient.Latest.People.WithId(personId).GetAsync();
        return response.Data;
    }
}
```

### Console Applications

For simple scenarios, create and reuse a single HttpClient instance:

```csharp
using Crews.PlanningCenter.Api.Authentication;
using System.Net.Http.Headers;

PlanningCenterPersonalAccessToken token = new() { AppId = "...", Secret = "..." };

var httpClient = new HttpClient
{
    BaseAddress = new Uri(PlanningCenterAuthenticationDefaults.BaseUrl)
};
httpClient.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));
httpClient.DefaultRequestHeaders.Authorization = token;

// Reuse httpClient for multiple requests
var peopleClient = new PeopleClient(httpClient).Latest;
```

## Fetching a Single Resource

```csharp
var peopleClient = new PeopleClient(httpClient).Latest;
var response = await peopleClient.People.WithId("123").GetAsync();

var person = response.Data;
Console.WriteLine($"Name: {person?.Attributes?.Name}");
```

## Working with Collections

Fetching multiple resources with pagination:

```csharp
var peopleClient = new PeopleClient(httpClient).Latest;
var response = await peopleClient.People.GetAsync();

foreach (var person in response.Data ?? [])
{
    Console.WriteLine($"{person.Id}: {person.Attributes?.Name}");
}
```

Use pagination parameters to control page size and offset:

```csharp
var response = await peopleClient.People
    .PerPage(25)
    .Offset(50)
    .GetAsync();
```

## Filtering Results

Use the `Filter()` method to narrow results. Supported filter values are defined in the Planning Center API documentation for each resource:

```csharp
var response = await peopleClient.People
    .Filter("admins")
    .GetAsync();
```

Multiple filters can be chained and are combined additively:

```csharp
var response = await peopleClient.People
    .Filter("admins")
    .Filter("peopleClientanization_admins")
    .GetAsync();
```

## Including Related Resources

Use `Include*()` methods to sideload related resources in a single request. The included resources are returned alongside the primary data in the response:

```csharp
var response = await peopleClient.People.WithId("123")
    .IncludeAddresses()
    .IncludeEmails()
    .GetAsync();

var person = response.Data;
var addresses = response.ResponseBody?.Included?.OfType<AddressResource>();
```

Multiple `Include*()` calls can be chained on the same request.

## Ordering Results

Use `OrderBy*()` methods to sort collection results by a specific field in ascending order. Each field also has a corresponding `OrderBy*Descending()` method for reverse ordering:

```csharp
// Ascending
var response = await peopleClient.People
    .OrderByLastName()
    .GetAsync();

// Descending
var response = await peopleClient.People
    .OrderByLastNameDescending()
    .GetAsync();
```

Only one `OrderBy*()` call takes effect — the last one wins if multiple are chained.

## Filtering by Field Value

Use `Where*()` methods to filter results by an exact field value. The method name and parameter type match the field being filtered:

```csharp
var response = await peopleClient.People
    .WhereLastName("Smith")
    .GetAsync();
```

Multiple `Where*()` calls can be chained and are combined additively:

```csharp
var response = await peopleClient.People
    .WhereLastName("Smith")
    .WhereGender("M")
    .GetAsync();
```

## Custom Query Parameters

For query parameters not directly surfaced by the client, use `AddCustomParameter`:

```csharp
var response = await peopleClient.People
    .AddCustomParameter("where[search_name]", "Smith")
    .GetAsync();
```

## Error Handling

See the [Error Handling](error-handling.md) guide for detailed information on handling API errors.

## Best Practices

1. **Use dependency injection** - Leverage `IHttpClientFactory` in ASP.NET Core
2. **Add resilience policies** - Use `.AddStandardResilienceHandler()` or custom Polly policies
3. **Handle rate limits** - Implement retry logic with exponential backoff
4. **Cache responses** - Cache appropriate responses to reduce API calls
5. **Dispose resources** - Properly dispose of HttpClient instances when not using DI
