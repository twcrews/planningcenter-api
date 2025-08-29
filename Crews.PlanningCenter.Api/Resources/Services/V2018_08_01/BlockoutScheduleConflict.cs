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
/// A fetchable Services BlockoutScheduleConflict resource.
/// </summary>
public class BlockoutScheduleConflictResource
  : PlanningCenterSingletonFetchableResource<BlockoutScheduleConflict, BlockoutScheduleConflictResource, ServicesDocumentContext>
{

  internal BlockoutScheduleConflictResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services BlockoutScheduleConflict resources.
/// </summary>
public class BlockoutScheduleConflictResourceCollection
  : PlanningCenterPaginatedFetchableResource<BlockoutScheduleConflict, BlockoutScheduleConflictResourceCollection, BlockoutScheduleConflictResource, ServicesDocumentContext>
{
  internal BlockoutScheduleConflictResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

