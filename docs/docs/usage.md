# Using the API Client

Common patterns and examples for using the Planning Center API client library.

## Client Lifetime Management

### ASP.NET Core (Recommended)

Always use `IHttpClientFactory` for proper HttpClient lifetime management:

```csharp
public class PeopleService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PeopleService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<PersonResource> GetPersonAsync(string personId)
    {
        var httpClient = _httpClientFactory.CreateClient("PlanningCenterApi");
        var client = new PersonClient(httpClient,
            new Uri($"/people/v2/people/{personId}", UriKind.Relative));

        var response = await client.GetAsync();
        return response.Data;
    }
}
```

### Console Applications

For simple scenarios, create and reuse a single HttpClient instance:

```csharp
using var httpClient = new HttpClient()
    .ConfigureForPlanningCenter()
    .AddPlanningCenterAuth(token);

// Use httpClient for multiple requests
```

## Working with Collections

Fetching multiple resources with pagination:

```csharp
var peopleClient = new PeopleClient(httpClient,
    new Uri("/people/v2/people", UriKind.Relative));

var response = await peopleClient.GetAsync();

foreach (var person in response.Data)
{
    Console.WriteLine($"{person.Id}: {person.Attributes.Name}");
}

// Handle pagination
if (response.Links?.Next != null)
{
    // Fetch next page
}
```

## Filtering and Querying

Use query parameters to filter and refine results:

```csharp
// TODO: Add examples once query parameter support is documented
```

## Including Related Resources

Include related resources in a single request:

```csharp
// TODO: Add examples once include support is documented
```

## Error Handling

See the [Error Handling](error-handling.md) guide for detailed information on handling API errors.

## Best Practices

1. **Use dependency injection** - Leverage `IHttpClientFactory` in ASP.NET Core
2. **Add resilience policies** - Use `.AddStandardResilienceHandler()` or custom Polly policies
3. **Handle rate limits** - Implement retry logic with exponential backoff
4. **Cache responses** - Cache appropriate responses to reduce API calls
5. **Dispose resources** - Properly dispose of HttpClient instances when not using DI
