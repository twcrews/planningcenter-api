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
/// A fetchable Services MediaSchedule resource.
/// </summary>
public class MediaScheduleResource
  : PlanningCenterSingletonFetchableResource<MediaSchedule, MediaScheduleResource, ServicesDocumentContext>
{

  internal MediaScheduleResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services MediaSchedule resources.
/// </summary>
public class MediaScheduleResourceCollection
  : PlanningCenterPaginatedFetchableResource<MediaSchedule, MediaScheduleResourceCollection, MediaScheduleResource, ServicesDocumentContext>
{
  internal MediaScheduleResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

