/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2024_09_12.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2024_09_12;

/// <summary>
/// A fetchable People SpamEmailAddress resource.
/// </summary>
public class SpamEmailAddressResource
  : PlanningCenterSingletonFetchableResource<SpamEmailAddress, SpamEmailAddressResource, PeopleDocumentContext>
{

  internal SpamEmailAddressResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People SpamEmailAddress resources.
/// </summary>
public class SpamEmailAddressResourceCollection
  : PlanningCenterPaginatedFetchableResource<SpamEmailAddress, SpamEmailAddressResourceCollection, SpamEmailAddressResource, PeopleDocumentContext>
{
  internal SpamEmailAddressResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

