# Integration Tests

This project contains integration tests for the Planning Center API library. These tests make real API calls to Planning Center and require valid credentials.

## Configuration

Integration tests require Planning Center API credentials. You can configure them using one of these methods:

### Option 1: User Secrets (Recommended for local development)

```bash
cd Crews.PlanningCenter.Api.IntegrationTests
dotnet user-secrets set "PlanningCenterClient:PersonalAccessToken:AppId" "your-app-id"
dotnet user-secrets set "PlanningCenterClient:PersonalAccessToken:Secret" "your-secret"
```

### Option 2: Environment Variables (Recommended for CI/CD)

Set these environment variables:

```bash
export PLANNINGCENTER_AppId="your-app-id"
export PLANNINGCENTER_Secret="your-secret"
```

Or on Windows:

```powershell
$env:PLANNINGCENTER_AppId="your-app-id"
$env:PLANNINGCENTER_Secret="your-secret"
```

### Option 3: Configuration File (NOT recommended - easily committed by mistake)

Copy `appsettings.json.example` to `appsettings.json` and fill in your credentials:

```json
{
  "PlanningCenterClient": {
    "PersonalAccessToken": {
      "AppId": "your-app-id",
      "Secret": "your-secret"
    }
  }
}
```

**Important:** Never commit `appsettings.json` with real credentials!

## Getting Credentials

To get Planning Center API credentials:

1. Go to [Planning Center API](https://api.planningcenteronline.com/oauth/applications)
2. Create a new Personal Access Token
3. Copy the Application ID and Secret

## Running Tests

### Run all integration tests

```bash
# From repository root
dotnet test --filter "FullyQualifiedName~IntegrationTests"

# Or from the integration test project directory
cd Crews.PlanningCenter.Api.IntegrationTests
dotnet test
```

### Run specific test classes

```bash
# People API tests
dotnet test --filter "FullyQualifiedName~PeopleClientIntegrationTests"

# Authentication tests
dotnet test --filter "FullyQualifiedName~PersonalAccessTokenIntegrationTests"

# Fluent API tests
dotnet test --filter "FullyQualifiedName~FluentApiIntegrationTests"
```

### Run by category

```bash
# All integration tests
dotnet test --filter "Category=Integration"
```

### Skip tests without credentials

If credentials are not configured, tests will automatically be skipped with a message like:

```
Planning Center credentials not configured. Set user secrets or environment variables.
```

## Test Organization

```
Crews.PlanningCenter.Api.IntegrationTests/
├── Infrastructure/           # Base classes and test helpers
│   ├── IntegrationTestBase.cs
│   ├── PlanningCenterClientFixture.cs
│   └── TestHelpers.cs
├── Authentication/           # Authentication mechanism tests
│   └── PersonalAccessTokenIntegrationTests.cs
├── Clients/                  # Product-specific client tests
│   └── PeopleClientIntegrationTests.cs
└── Features/                 # Feature-specific tests
    └── FluentApiIntegrationTests.cs
```

## What's Tested

### Authentication
- Personal Access Token authentication
- Standalone client pattern
- Multiple requests with same credentials
- Invalid credential handling

### People API Client
- Fetching current user
- Fetching people collections
- Fetching single person by ID
- Including related resources
- Ordering results
- Querying/filtering
- Pagination
- API version selection
- Complex fluent queries

### Fluent API Features
- Single and multiple includes
- Ascending/descending ordering
- Single and multiple query parameters
- Pagination (count, offset, metadata)
- Chaining operations (Include + OrderBy + Query + Pagination)

## Adding New Tests

To add integration tests for a new product API:

1. Create a new test class in `Clients/` directory
2. Inherit from or use `PlanningCenterClientFixture`
3. Mark tests with `[Trait("Category", "Integration")]`
4. Use `Skip.IfNot(_fixture.HasCredentials(), ...)` to skip when not configured
5. Follow existing test patterns

Example:

```csharp
[Trait("Category", "Integration")]
public class CalendarClientIntegrationTests : IClassFixture<PlanningCenterClientFixture>
{
    private readonly PlanningCenterClientFixture _fixture;

    public CalendarClientIntegrationTests(PlanningCenterClientFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Can fetch events")]
    public async Task GetEvents_ReturnsCollection()
    {
        Skip.IfNot(_fixture.HasCredentials(), "Credentials not configured");

        var result = await _fixture.Client.Calendar.LatestVersion
            .Events
            .GetAllAsync(count: 5);

        Assert.NotNull(result);
        TestHelpers.AssertResourceCollection<Event>(result.JsonApiDocument);
    }
}
```

## CI/CD Integration

For GitHub Actions, add secrets to your repository:

```yaml
- name: Run Integration Tests
  env:
    PLANNINGCENTER_AppId: ${{ secrets.PLANNINGCENTER_APPID }}
    PLANNINGCENTER_Secret: ${{ secrets.PLANNINGCENTER_SECRET }}
  run: dotnet test --filter "Category=Integration"
```

## Rate Limiting

The Planning Center API has rate limits. Integration tests:
- Use shared fixtures to minimize client instances
- Request small result sets (typically count: 1-10)
- Should be run less frequently than unit tests

Consider:
- Running integration tests only on PR merges or nightly builds
- Using test-specific organization if available
- Implementing retry logic with exponential backoff for rate limit errors

## Troubleshooting

### Tests are skipped
- Verify credentials are configured correctly
- Check environment variable names have `PLANNINGCENTER_` prefix
- Run `dotnet user-secrets list` to verify user secrets

### Authentication errors (401)
- Verify your credentials are valid
- Check if the token has necessary scopes/permissions
- Try using the credentials in a standalone HTTP client

### API errors
- Check Planning Center API status
- Review rate limiting policies
- Verify your organization has access to the API products being tested

### Tests timeout
- Planning Center API may be slow or rate-limiting
- Consider increasing test timeout: `[Fact(Timeout = 30000)]`
- Check network connectivity

## Best Practices

1. **Always skip when credentials missing** - Use `Skip.IfNot()` pattern
2. **Request minimal data** - Use `count: 1-10` to reduce API load
3. **Don't rely on specific data** - Tests should work with any organization
4. **Clean up after destructive tests** - Delete created resources
5. **Use descriptive test names** - Clearly state what's being tested
6. **Leverage fixtures** - Share client instances across tests
7. **Test happy paths** - Focus on integration, not edge cases (those go in unit tests)

## Future Enhancements

Potential improvements for integration tests:

- [ ] Add tests for Calendar, CheckIns, Giving, Groups, Publishing, Services APIs
- [ ] Add OAuth/OIDC authentication tests (requires test web application)
- [ ] Add mutation tests (POST/PATCH/DELETE) with proper cleanup
- [ ] Add custom resource pattern tests
- [ ] Add error handling tests (rate limits, 404, etc.)
- [ ] Add performance benchmarks
- [ ] Record/replay mode for offline testing (VCR pattern)
