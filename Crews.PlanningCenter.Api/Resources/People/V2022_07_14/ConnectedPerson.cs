/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2022_07_14.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2022_07_14;

/// <summary>
/// A fetchable People ConnectedPerson resource.
/// </summary>
public class ConnectedPersonResource
  : PlanningCenterSingletonFetchableResource<ConnectedPerson, ConnectedPersonResource, PeopleDocumentContext>
{

  internal ConnectedPersonResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People ConnectedPerson resources.
/// </summary>
public class ConnectedPersonResourceCollection
  : PlanningCenterPaginatedFetchableResource<ConnectedPerson, ConnectedPersonResourceCollection, ConnectedPersonResource, PeopleDocumentContext>
{
  internal ConnectedPersonResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

