/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2023_02_15.Entities;
using Crews.PlanningCenter.Models.People.V2023_02_15.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2023_02_15;

/// <summary>
/// A fetchable People ListStar resource.
/// </summary>
public class ListStarResource
  : PlanningCenterSingletonFetchableResource<ListStar, ListStarResource, PeopleDocumentContext>
{

  internal ListStarResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People ListStar resources.
/// </summary>
public class ListStarResourceCollection
  : PlanningCenterPaginatedFetchableResource<ListStar, ListStarResourceCollection, ListStarResource, PeopleDocumentContext>
{
  internal ListStarResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

