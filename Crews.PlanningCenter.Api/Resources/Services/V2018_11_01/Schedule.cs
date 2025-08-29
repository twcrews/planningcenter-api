/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_11_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_11_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_11_01;

/// <summary>
/// A fetchable Services Schedule resource.
/// </summary>
public class ScheduleResource
  : PlanningCenterSingletonFetchableResource<Schedule, ScheduleResource, ServicesDocumentContext>,
  IIncludable<ScheduleResource, ScheduleIncludable>
{

  /// <summary>
  /// The related <see cref="PlanTimeResourceCollection" />.
  /// </summary>
  public PlanTimeResourceCollection DeclinedPlanTimes => GetRelated<PlanTimeResourceCollection>("declined_plan_times");

  /// <summary>
  /// The related <see cref="PlanTimeResourceCollection" />.
  /// </summary>
  public PlanTimeResourceCollection PlanTimes => GetRelated<PlanTimeResourceCollection>("plan_times");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource RespondTo => GetRelated<PersonResource>("respond_to");

  /// <summary>
  /// The related <see cref="TeamResource" />.
  /// </summary>
  public TeamResource Team => GetRelated<TeamResource>("team");

  internal ScheduleResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public ScheduleResource Include(params ScheduleIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Services Schedule resources.
/// </summary>
public class ScheduleResourceCollection
  : PlanningCenterPaginatedFetchableResource<Schedule, ScheduleResourceCollection, ScheduleResource, ServicesDocumentContext>,
  IIncludable<ScheduleResourceCollection, ScheduleIncludable>,
  IOrderable<ScheduleResourceCollection, ScheduleOrderable>,
  IQueryable<ScheduleResourceCollection, ScheduleQueryable>
{
  internal ScheduleResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ScheduleResourceCollection Include(params ScheduleIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public ScheduleResourceCollection OrderBy(ScheduleOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public ScheduleResourceCollection Query(params (ScheduleQueryable, string)[] queries)
    => base.Query(queries);
}

