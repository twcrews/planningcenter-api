/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services Attendance resource.
/// </summary>
public class AttendanceResource
  : PlanningCenterSingletonFetchableResource<Attendance, AttendanceResource, ServicesDocumentContext>
{

  internal AttendanceResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services Attendance resources.
/// </summary>
public class AttendanceResourceCollection
  : PlanningCenterPaginatedFetchableResource<Attendance, AttendanceResourceCollection, AttendanceResource, ServicesDocumentContext>
{
  internal AttendanceResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

