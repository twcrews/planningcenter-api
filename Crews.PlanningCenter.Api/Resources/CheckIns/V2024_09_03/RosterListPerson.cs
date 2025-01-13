/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2024_09_03.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2024_09_03;

/// <summary>
/// A fetchable CheckIns RosterListPerson resource.
/// </summary>
public class RosterListPersonResource
  : PlanningCenterSingletonFetchableResource<RosterListPerson, RosterListPersonResource, CheckInsDocumentContext>
{

  internal RosterListPersonResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of CheckIns RosterListPerson resources.
/// </summary>
public class RosterListPersonResourceCollection
  : PlanningCenterPaginatedFetchableResource<RosterListPerson, RosterListPersonResourceCollection, RosterListPersonResource, CheckInsDocumentContext>
{
  internal RosterListPersonResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

