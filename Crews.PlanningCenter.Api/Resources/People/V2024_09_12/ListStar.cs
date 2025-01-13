/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2024_09_12.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2024_09_12;

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

