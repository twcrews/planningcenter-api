# Rate Limiting

How to read rate limit information from API responses and handle limits in your application.

## Rate Limit Headers

Every Planning Center API response includes headers that describe the current rate limit window:

| Header | Description |
|---|---|
| `X-PCO-API-Request-Rate-Limit` | Maximum number of requests allowed per window |
| `X-PCO-API-Request-Rate-Period` | Duration of the window (e.g., `"20 seconds"`) |

Refer to the [Planning Center API documentation](https://developer.planning.center/docs/) for current rate limit policies, as these are subject to change.

## Reading Rate Limit Headers

Every response exposes the raw `HttpResponseMessage` via `response.ResponseMessage`. Use it to read rate limit headers after any request:

```csharp
var response = await org.People.GetAsync();

var headers = response.ResponseMessage?.Headers;

if (headers?.TryGetValues("X-PCO-API-Request-Rate-Limit", out var limitVals) == true
    && int.TryParse(limitVals.FirstOrDefault(), out var limit))
{
    Console.WriteLine($"Rate limit: {limit} requests per window");
}

if (headers?.TryGetValues("X-PCO-API-Request-Rate-Period", out var periodVals) == true)
{
    Console.WriteLine($"Window: {periodVals.FirstOrDefault()}");
}
```

## Handling 429 Responses

When the rate limit is exceeded the API returns `429 Too Many Requests` and the library throws a `JsonApiException`. The response also includes a `Retry-After` header indicating how long to wait before retrying.

```csharp
catch (JsonApiException ex) when (ex.StatusCode == HttpStatusCode.TooManyRequests)
{
    var retryAfter = ex.HttpRequestError; // inspect ResponseMessage directly if needed
    // Wait and retry
}
```

For production usage, use `.AddStandardResilienceHandler()` when registering your HttpClient — it handles 429 retries automatically:

```csharp
builder.Services.AddHttpClient("PlanningCenterApi", client => { /* configure */ })
    .AddStandardResilienceHandler();
```

## Proactive Throttling with a Delegating Handler

If you need fine-grained control — for example, to avoid hitting limits at all rather than retrying after — implement a `DelegatingHandler` that tracks the request window and throttles proactively.

The integration test suite includes a reference implementation at
[`Crews.PlanningCenter.Api.IntegrationTests/Infrastructure/RateLimitHandler.cs`](../../Crews.PlanningCenter.Api.IntegrationTests/Infrastructure/RateLimitHandler.cs).
It reads `X-PCO-API-Request-Rate-Limit` and `X-PCO-API-Request-Rate-Period` from each response, tracks requests in the current window, and delays outgoing requests when the limit is approached. It also retries automatically on 429 responses using the `Retry-After` header.

Wire it up as the outermost handler in your HttpClient pipeline:

```csharp
var httpClient = new HttpClient(new RateLimitHandler { InnerHandler = new HttpClientHandler() })
{
    BaseAddress = new Uri(PlanningCenterAuthenticationDefaults.BaseUrl)
};
```
