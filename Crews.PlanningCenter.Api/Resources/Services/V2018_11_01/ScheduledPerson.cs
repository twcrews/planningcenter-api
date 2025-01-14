/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_11_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_11_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_11_01;

/// <summary>
/// A fetchable Services ScheduledPerson resource.
/// </summary>
public class ScheduledPersonResource
  : PlanningCenterSingletonFetchableResource<ScheduledPerson, ScheduledPersonResource, ServicesDocumentContext>
{

  internal ScheduledPersonResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services ScheduledPerson resources.
/// </summary>
public class ScheduledPersonResourceCollection
  : PlanningCenterPaginatedFetchableResource<ScheduledPerson, ScheduledPersonResourceCollection, ScheduledPersonResource, ServicesDocumentContext>
{
  internal ScheduledPersonResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

