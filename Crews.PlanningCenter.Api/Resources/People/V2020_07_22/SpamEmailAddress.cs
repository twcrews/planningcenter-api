/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2020_07_22.Entities;
using Crews.PlanningCenter.Models.People.V2020_07_22.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2020_07_22;

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

