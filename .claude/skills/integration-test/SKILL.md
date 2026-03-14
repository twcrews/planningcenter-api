---
name: integration-test
description: Write integration tests for the Planning Center API client library. Use when the user asks to add, write, or create integration tests for any Planning Center product (Calendar, Check-Ins, Giving, Groups, People, Publishing, Registrations, Services) or asks about the integration test patterns.
disable-model-invocation: false
---

# Writing Integration Tests

Integration tests live in `Crews.PlanningCenter.Api.IntegrationTests/Products/{Product}/`. Each test file covers one resource type.

## Before Writing Tests

1. Identify which **product** the resource belongs to (e.g., People, Groups, Calendar)
2. Check what the fixture already pre-creates — see `Infrastructure/ProductFixtures/{Product}Fixture.cs`
3. Determine if the resource supports CRUD or read-only operations by checking the generated client in `obj/Generated/`
4. Check whether the resource needs a parent resource (e.g., Email lives under Person)

## File Structure

```
Products/{Product}/{ResourceName}Tests.cs
```

The class inherits from `{Product}TestBase` and takes `{Product}Fixture` as a constructor parameter.

## Test Patterns

### Pattern 1: Full CRUD Lifecycle

Use when the resource supports create, read, update, and delete.
See [examples/crud-test.cs](examples/crud-test.cs).

Key rules:
- Declare `string? resourceId = null` before the try block
- Set `resourceId = null` immediately after a successful delete
- In the `finally` block, attempt best-effort cleanup if `resourceId is not null`
- Assert on `createResult.Data` and at least one attribute after create
- Assert on `readResult.Data` and `readResult.Data.Id` after read
- Assert on `updateResult.Data` after update
- Read again after update to verify with `verifyResult.Data?.Attributes?.PropertyName`

### Pattern 2: Read-Only (Get)

Use when the resource only supports GET, or when you only need to verify read access.
See [examples/readonly-test.cs](examples/readonly-test.cs).

Key rules:
- Use `CollectionReadHelper.GetFirstIdAsync(HttpClient, "{product}/v2/{collection_path}")` to discover an ID
- Assert `result.Data` is not null and `result.ResponseMessage?.IsSuccessStatusCode` is true
- No cleanup needed

### Pattern 3: Child Resource CRUD

Use when the resource is nested under a parent that the fixture pre-creates.
See [examples/child-crud-test.cs](examples/child-crud-test.cs).

Key rules:
- Navigate via `Org.{ParentResources}.WithId(Fixture.{ParentId}).{ChildResources}`
- Same try/finally cleanup as Pattern 1, but include the full navigation path in the finally block
- Use `Fixture.{ParentId}` for the parent — never create a parent in the test itself

### Pattern 4: Relationship-Required Resource (JsonApiDocument)

Use when the POST body requires `Relationships` (e.g., WorkflowCard requires a Person).
See [examples/relationship-test.cs](examples/relationship-test.cs).

Key rules:
- Pass `new JsonApiDocument<{Resource}Resource> { Data = new() { Attributes = ..., Relationships = ... } }` to PostAsync
- Use `Fixture.{RelatedResourceId}` for relationship IDs
- Add `using Crews.Web.JsonApiClient;` to imports

## Naming Conventions

- Test method: `{ResourceName}_{OperationDescription}` (e.g., `Person_FullCrudLifecycle`, `App_GetAsync_ReturnsApp`)
- Use `UniqueId` (8-char random suffix from base class) to avoid name collisions: `$"IntTest-{UniqueId}"`
- Prefix test data names with `"IntTest"` or `"IntTest-Updated"` for clarity

## Standard Imports

```csharp
using Crews.PlanningCenter.Api.{Product}.V{Version};
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;
```

Add `using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;` when using `CollectionReadHelper`.
Add `using Crews.Web.JsonApiClient;` when using `JsonApiDocument<T>`.

## Namespace

```csharp
namespace Crews.PlanningCenter.Api.IntegrationTests.Products.{Product};
```

## Available Fixture Properties

### PeopleFixture
- `PersonId` — pre-created Person
- `HouseholdId` — pre-created Household (with PersonId as primary contact)
- `WorkflowId` — pre-created Workflow
- `NoteCategoryId` — pre-created NoteCategory
- `TabId` — pre-created Tab
- `CampusId` — pre-created Campus
- `FieldDefinitionId` — pre-created FieldDefinition (under TabId)
- `ListId` — first available List (nullable — may be null if no lists exist)

### CalendarFixture
Check `Infrastructure/ProductFixtures/CalendarFixture.cs` for current properties.

### Other fixtures
Check `Infrastructure/ProductFixtures/{Product}Fixture.cs` for available properties.

## CollectionReadHelper

Used to dynamically discover resource IDs at test time:

```csharp
// Get first item in a collection
var id = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "people/v2/apps");

// Get last item (useful when you want a stable/mature resource)
var id = await CollectionReadHelper.GetLastIdAsync(HttpClient, "groups/v2/group_types");
```

The path format is `{product}/v2/{resource_plural}`.

## After Writing the Test

Build and run just the new test to verify it passes:
```bash
dotnet test Crews.PlanningCenter.Api.IntegrationTests --filter "FullyQualifiedName~{TestClassName}"
```
