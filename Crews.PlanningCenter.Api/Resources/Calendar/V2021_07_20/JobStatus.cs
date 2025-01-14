/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2021_07_20.Entities;
using Crews.PlanningCenter.Models.Calendar.V2021_07_20.Parameters;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2021_07_20;

/// <summary>
/// A fetchable Calendar JobStatus resource.
/// </summary>
public class JobStatusResource
  : PlanningCenterSingletonFetchableResource<JobStatus, JobStatusResource, CalendarDocumentContext>
{

  internal JobStatusResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Calendar JobStatus resources.
/// </summary>
public class JobStatusResourceCollection
  : PlanningCenterPaginatedFetchableResource<JobStatus, JobStatusResourceCollection, JobStatusResource, CalendarDocumentContext>
{
  internal JobStatusResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

