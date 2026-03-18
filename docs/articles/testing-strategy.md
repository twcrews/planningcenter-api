# Testing Strategy

This article describes the testing approach used in this library, with a focus on integration tests that run against the live Planning Center API.

## Test Projects

The solution contains five test projects:

| Project | Purpose |
|---------|---------|
| `Crews.PlanningCenter.Api.Tests` | Unit tests for the main library |
| `Crews.PlanningCenter.Api.DocParser.Tests` | Unit tests for the documentation parser |
| `Crews.PlanningCenter.Api.Generators.Tests` | Unit tests for the source generators |
| `Crews.PlanningCenter.Api.Models.Tests` | Unit tests for the shared model definitions |
| `Crews.PlanningCenter.Api.IntegrationTests` | Integration tests against the live API |

## Integration Tests

Integration tests verify end-to-end behavior of the library against the real Planning Center API — no mocks, no stubs. Every test makes live HTTP requests and asserts on real responses, which ensures that the generated client code, authentication, serialization, and request routing all work correctly together.

### Credentials

Integration tests require a Planning Center Personal Access Token. Configure via any of:

- **User secrets** (recommended for local development):
  ```bash
  cd Crews.PlanningCenter.Api.IntegrationTests
  dotnet user-secrets set "PlanningCenter:AppId" "your-app-id"
  dotnet user-secrets set "PlanningCenter:Secret" "your-secret"
  ```
- **Environment variables**: `PlanningCenter__AppId` and `PlanningCenter__Secret`
- **appsettings.json** (not recommended — avoid committing credentials)

### Running Integration Tests

```bash
dotnet test Crews.PlanningCenter.Api.IntegrationTests
```

### Infrastructure

The integration test infrastructure is in `Infrastructure/`:

#### `PlanningCenterFixture`

An XUnit `IAsyncLifetime` fixture that creates and configures an authenticated `HttpClient` before tests run and disposes it after. All tests share a single `HttpClient` instance per product collection via XUnit's collection fixture mechanism.

#### `RateLimitHandler`

A delegating handler that wraps the `HttpClient` and prevents the test suite from exceeding Planning Center's rate limits. It:

- Reads `X-PCO-API-Request-Rate-Limit` and `X-PCO-API-Request-Rate-Period` response headers to dynamically learn the current limit and window
- Proactively throttles outgoing requests when the window is nearly exhausted
- Retries automatically on `429 Too Many Requests` responses

A shared static window ensures all product fixture instances, which may run in parallel, are throttled against a single counter.

#### Per-Product Fixtures

Some products require shared parent resources that multiple test classes need. Per-product fixtures (e.g., `CalendarFixture`, `ServicesFixture`) extend `PlanningCenterFixture` and create these resources during `InitializeAsync`, cleaning them up in `DisposeAsync`.

For resources that cannot be created by the test suite (e.g., events that must already exist in the account), the fixture fetches an existing ID from the collection endpoint using `CollectionReadHelper`.

#### Per-Product Collections and Test Bases

Each product has:
- A collection definition (e.g., `CalendarCollection`) that shares a single fixture instance across all test classes in the product
- An abstract test base (e.g., `CalendarTestBase`) that exposes the `HttpClient`, fixture, and a root `OrganizationClient` to tests

### Test Structure

Test classes follow a consistent pattern:

```csharp
public class EventTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
    [Fact]
    public async Task Event_GetAsync_ReturnsEvent()
    {
        var result = await Org.Events.WithId(Fixture.EventId).GetAsync();

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
    }
}
```

Tests are organized under `Products/{ProductName}/` and tagged with `[Trait("Product", "...")]` for filtering.

### Using Real Data

Integration tests operate against real data in a live Planning Center account. This has a few practical implications:

- **Read tests** fetch existing resources and assert that the response is successful and non-null. They do not assert on specific field values, since the underlying data can change.
- **Write tests** create resources during the test (or in the fixture's `InitializeAsync`) and clean them up in `DisposeAsync`. Resources are named with a short random suffix (e.g., `Fixture-TG-a3f92c1b`) to avoid collisions across concurrent runs.
- **Tests that rely on existing data** (e.g., reading an event) use `CollectionReadHelper` to fetch the first available ID from the relevant collection endpoint before the test suite begins. If no data exists in the account, the fixture will receive a null ID and the dependent tests will fail.

## Endpoint Limitations

Not all API endpoints can be tested. Several categories of endpoints are excluded:

### Paywalled Products and Features

Certain Planning Center products — or specific endpoints within a product — require a paid subscription tier that the test account may not have. Attempting to call these endpoints returns a `402 Payment Required` or `403 Forbidden` response. Tests for these endpoints are omitted rather than written to assert on failure.

### Write-Only or Destructive Operations

Some endpoints perform irreversible actions (e.g., sending a notification, finalizing a batch). These are excluded from the test suite to avoid unintended side effects in the live account.

### Endpoints Requiring External State

Some endpoints only return data under specific conditions that cannot be reliably reproduced in a test environment. Tests for these endpoints are skipped or omitted when there is no practical way to seed the required state.
