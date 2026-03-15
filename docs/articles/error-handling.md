# Error Handling

How to handle errors and exceptions when using the Planning Center API client library.

## Exception Types

### JsonApiException

The library throws `JsonApiException` for API-level errors (non-2xx HTTP responses). `JsonApiException` extends `HttpRequestException` and exposes the parsed JSON:API error details:

```csharp
using Crews.PlanningCenter.Api.Models;
using System.Net;

try
{
    var response = await org.People.WithId("123").GetAsync();
}
catch (JsonApiException ex)
{
    Console.WriteLine($"API Error: {ex.Message}");
    Console.WriteLine($"Status Code: {ex.StatusCode}");

    // Inspect individual JSON:API errors from the response body
    foreach (var error in ex.Errors)
    {
        Console.WriteLine($"  [{error.Status}] {error.Title}: {error.Detail}");
    }
}
```

### HttpRequestException

`JsonApiException` is a subclass of `HttpRequestException`, so you can catch it as `HttpRequestException` for network and HTTP-level errors. Catch `JsonApiException` first if you need access to the parsed `Errors` collection:

```csharp
catch (JsonApiException ex)
{
    // API returned a non-2xx status with a JSON:API error body
}
catch (HttpRequestException ex)
{
    // Network failure or other HTTP error without a JSON:API body
    Console.WriteLine($"Network Error: {ex.Message}");
}
```

## Common Error Scenarios

### Authentication Errors (401)

```csharp
catch (JsonApiException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
{
    // Invalid or expired credentials
}
```

### Authorization Errors (403)

```csharp
catch (JsonApiException ex) when (ex.StatusCode == HttpStatusCode.Forbidden)
{
    // Insufficient permissions — request appropriate scopes
}
```

### Not Found Errors (404)

```csharp
catch (JsonApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
{
    // Resource does not exist
}
```

### Rate Limiting (429)

```csharp
catch (JsonApiException ex) when (ex.StatusCode == HttpStatusCode.TooManyRequests)
{
    // Rate limit exceeded — implement exponential backoff and retry
}
```

## Resilience Patterns

### Using Built-in Resilience Handler (.NET 8+)

When registering your HttpClient in ASP.NET Core, add the standard resilience handler for automatic retry and circuit-breaker behavior:

```csharp
builder.Services.AddHttpClient("PlanningCenterApi", client =>
{
    client.BaseAddress = new Uri(PlanningCenterAuthenticationDefaults.BaseUrl);
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Authorization = token;
})
.AddStandardResilienceHandler();
```

## Best Practices

1. **Always handle exceptions** - Don't let unhandled exceptions crash your application
2. **Catch `JsonApiException` before `HttpRequestException`** - `JsonApiException` is a subclass; catching the base type first swallows API error details
3. **Use `HttpStatusCode` enum values** - Compare `ex.StatusCode` to `HttpStatusCode.NotFound`, `HttpStatusCode.Unauthorized`, etc.
4. **Inspect `ex.Errors`** - The `Errors` collection contains structured error information from the API response body
5. **Implement retry logic** - Use `.AddStandardResilienceHandler()` or Polly for transient errors
6. **Handle rate limits** - Respect API rate limits and implement appropriate backoff
