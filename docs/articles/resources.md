# Working with Resources

Understanding resource types and how to work with them in the Planning Center API.

## Resource Structure

Each generated resource record wraps a JSON:API resource. For example, a `PersonResource` contains:

- `Id` — the resource's string identifier
- `Type` — the JSON:API type string
- `Attributes` — a `Person` record with all resource attributes
- `Relationships` — optional relationships to other resources

Access attributes through the response:

```csharp
var org = new PeopleClient(httpClient).Latest;
var response = await org.People.WithId("123").GetAsync();

var person = response.Data;
Console.WriteLine($"Name: {person?.Attributes?.Name}");
Console.WriteLine($"First: {person?.Attributes?.FirstName}");
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

Each product exposes multiple API versions through the root product client. Use `.Latest` for the current version or a specific version property:

```csharp
// Latest version (recommended)
var org = new PeopleClient(httpClient).Latest;

// Specific version
var org = new PeopleClient(httpClient).V2025_11_10;
```

Version namespaces use underscored date format (e.g., `V2025_11_10`).

## Reading a Single Resource

```csharp
var org = new PeopleClient(httpClient).Latest;
var response = await org.People.WithId("123").GetAsync();
var person = response.Data;
```

## Reading a Collection

```csharp
var org = new PeopleClient(httpClient).Latest;
var response = await org.People.GetAsync();

foreach (var person in response.Data ?? [])
{
    Console.WriteLine($"{person.Id}: {person.Attributes?.Name}");
}
```

## Creating Resources

Post to a collection client to create a new resource:

```csharp
var org = new PeopleClient(httpClient).Latest;

var createResponse = await org.People.WithId(personId).Addresses.PostAsync(new Address
{
    StreetLine1 = "123 Main St",
    City = "Springfield",
    State = "IL",
    Zip = "62701",
    Location = "Home"
});

var newAddress = createResponse.Data;
Console.WriteLine($"Created address ID: {newAddress?.Id}");
```

## Updating Resources

Use `PatchAsync` on a singleton client to update an existing resource. Only the attributes you set are sent:

```csharp
var org = new PeopleClient(httpClient).Latest;

var updateResponse = await org.People.WithId(personId).Addresses.WithId(addressId)
    .PatchAsync(new Address { Location = "Work" });
```

## Deleting Resources

```csharp
var org = new PeopleClient(httpClient).Latest;

await org.People.WithId(personId).Addresses.WithId(addressId).DeleteAsync();
```

## Navigating Related Resources

Resources expose nested clients for traversing related resources:

```csharp
var org = new PeopleClient(httpClient).Latest;

// Fetch all addresses for a person
var addresses = await org.People.WithId(personId).Addresses.GetAsync();

// Fetch all emails for a person
var emails = await org.People.WithId(personId).Emails.GetAsync();
```
