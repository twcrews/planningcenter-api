/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Groups.V2023_07_10.Entities;
using Crews.PlanningCenter.Models.Groups.V2023_07_10.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Groups.V2023_07_10;

/// <summary>
/// A fetchable Groups Enrollment resource.
/// </summary>
public class EnrollmentResource
  : PlanningCenterSingletonFetchableResource<Enrollment, EnrollmentResource, GroupsDocumentContext>
{

  internal EnrollmentResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Groups Enrollment resources.
/// </summary>
public class EnrollmentResourceCollection
  : PlanningCenterPaginatedFetchableResource<Enrollment, EnrollmentResourceCollection, EnrollmentResource, GroupsDocumentContext>
{
  internal EnrollmentResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

