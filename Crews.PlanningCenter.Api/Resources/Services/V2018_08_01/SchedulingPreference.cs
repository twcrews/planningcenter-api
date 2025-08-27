/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services SchedulingPreference resource.
/// </summary>
public class SchedulingPreferenceResource
  : PlanningCenterSingletonFetchableResource<SchedulingPreference, SchedulingPreferenceResource, ServicesDocumentContext>
{

  internal SchedulingPreferenceResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services SchedulingPreference resources.
/// </summary>
public class SchedulingPreferenceResourceCollection
  : PlanningCenterPaginatedFetchableResource<SchedulingPreference, SchedulingPreferenceResourceCollection, SchedulingPreferenceResource, ServicesDocumentContext>
{
  internal SchedulingPreferenceResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

