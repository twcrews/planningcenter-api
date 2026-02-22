# Working with Resources

Understanding resource types and how to work with them in the Planning Center API.

## Resource Structure

All Planning Center API resources follow a consistent structure:

```csharp
public record PersonResource
{
    public string Id { get; init; }
    public string Type { get; init; }
    public PersonAttributes Attributes { get; init; }
    public PersonRelationships? Relationships { get; init; }
    public ResourceLinks? Links { get; init; }
}
```

## Resource Attributes

Attributes contain the actual data for a resource:

```csharp
var person = response.Data;
Console.WriteLine($"Name: {person.Attributes.Name}");
Console.WriteLine($"Email: {person.Attributes.EmailAddresses?.FirstOrDefault()}");
```

## Resource Relationships

Relationships link resources together:

```csharp
// TODO: Add examples once relationship navigation is documented
```

## Resource Types

The library includes generated resource types for all Planning Center products:

- **People** - Person, Household, FieldDefinition, etc.
- **Check-Ins** - CheckIn, Event, Location, etc.
- **Giving** - Donation, Fund, Batch, etc.
- **Groups** - Group, Membership, Event, etc.
- **Services** - Plan, Team, Person, etc.
- **Calendar** - Event, Resource, Conflict, etc.
- **Registrations** - Event, Registration, Registrant, etc.
- **Publishing** - Episode, Channel, Asset, etc.

## API Versions

Each product may have multiple API versions. Use the appropriate version namespace:

```csharp
using Crews.PlanningCenter.Api.People.V2025_11_10;  // Latest People API version
using Crews.PlanningCenter.Api.Services.V2018_11_01; // Services API version
```

Version namespaces use underscored date format (e.g., `V2025_11_10`).

## Creating Resources

```csharp
// TODO: Add examples once resource creation is documented
```

## Updating Resources

```csharp
// TODO: Add examples once resource updates are documented
```

## Deleting Resources

```csharp
// TODO: Add examples once resource deletion is documented
```
