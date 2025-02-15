/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2024_09_03.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2024_09_03;

/// <summary>
/// A fetchable CheckIns CheckInTime resource.
/// </summary>
public class CheckInTimeResource
  : PlanningCenterSingletonFetchableResource<CheckInTime, CheckInTimeResource, CheckInsDocumentContext>
{

  internal CheckInTimeResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of CheckIns CheckInTime resources.
/// </summary>
public class CheckInTimeResourceCollection
  : PlanningCenterPaginatedFetchableResource<CheckInTime, CheckInTimeResourceCollection, CheckInTimeResource, CheckInsDocumentContext>
{
  internal CheckInTimeResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

