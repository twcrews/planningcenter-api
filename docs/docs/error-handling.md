# Error Handling

How to handle errors and exceptions when using the Planning Center API client library.

## Exception Types

### JsonApiException

The library throws `JsonApiException` for API-level errors:

```csharp
try
{
    var response = await client.GetAsync();
}
catch (JsonApiException ex)
{
    Console.WriteLine($"API Error: {ex.Message}");
    Console.WriteLine($"Status Code: {ex.StatusCode}");
    // Handle API error
}
```

### HttpRequestException

Network and HTTP-level errors throw `HttpRequestException`:

```csharp
try
{
    var response = await client.GetAsync();
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Network Error: {ex.Message}");
    // Handle network error
}
```

## Common Error Scenarios

### Authentication Errors (401)

```csharp
catch (JsonApiException ex) when (ex.StatusCode == 401)
{
    // Invalid or expired credentials
    // Re-authenticate or refresh token
}
```

### Authorization Errors (403)

```csharp
catch (JsonApiException ex) when (ex.StatusCode == 403)
{
    // Insufficient permissions
    // Request appropriate scopes or permissions
}
```

### Not Found Errors (404)

```csharp
catch (JsonApiException ex) when (ex.StatusCode == 404)
{
    // Resource not found
    // Handle missing resource gracefully
}
```

### Rate Limiting (429)

```csharp
catch (JsonApiException ex) when (ex.StatusCode == 429)
{
    // Rate limit exceeded
    // Implement exponential backoff and retry
}
```

## Resilience Patterns

### Using Built-in Resilience Handler (.NET 8+)

```csharp
builder.Services.AddHttpClient("PlanningCenterApi", client =>
{
    client.ConfigureForPlanningCenter()
          .AddPlanningCenterAuth(token);
})
.AddStandardResilienceHandler();
```

### Custom Retry Logic

```csharp
// TODO: Add custom retry examples
```

## Logging

Enable logging to diagnose issues:

```csharp
// TODO: Add logging configuration examples
```

## Best Practices

1. **Always handle exceptions** - Don't let unhandled exceptions crash your application
2. **Implement retry logic** - Use exponential backoff for transient errors
3. **Log errors** - Maintain error logs for debugging and monitoring
4. **Validate input** - Check parameters before making API calls
5. **Handle rate limits** - Respect API rate limits and implement appropriate backoff
6. **Monitor API health** - Track error rates and response times
