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
/// A fetchable Services SongSchedule resource.
/// </summary>
public class SongScheduleResource
  : PlanningCenterSingletonFetchableResource<SongSchedule, SongScheduleResource, ServicesDocumentContext>
{

  internal SongScheduleResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services SongSchedule resources.
/// </summary>
public class SongScheduleResourceCollection
  : PlanningCenterPaginatedFetchableResource<SongSchedule, SongScheduleResourceCollection, SongScheduleResource, ServicesDocumentContext>,
  IOrderable<SongScheduleResourceCollection, SongScheduleOrderable>
{
  internal SongScheduleResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public SongScheduleResourceCollection OrderBy(SongScheduleOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

