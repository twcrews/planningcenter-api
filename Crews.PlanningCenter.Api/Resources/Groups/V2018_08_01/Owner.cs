/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Groups.V2018_08_01.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Groups.V2018_08_01;

/// <summary>
/// A fetchable Groups Owner resource.
/// </summary>
public class OwnerResource
  : PlanningCenterSingletonFetchableResource<Owner, OwnerResource, GroupsDocumentContext>
{

  internal OwnerResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Groups Owner resources.
/// </summary>
public class OwnerResourceCollection
  : PlanningCenterPaginatedFetchableResource<Owner, OwnerResourceCollection, OwnerResource, GroupsDocumentContext>
{
  internal OwnerResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

