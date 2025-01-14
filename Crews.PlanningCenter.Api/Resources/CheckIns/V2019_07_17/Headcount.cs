/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2019_07_17.Entities;
using Crews.PlanningCenter.Models.CheckIns.V2019_07_17.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2019_07_17;

/// <summary>
/// A fetchable CheckIns Headcount resource.
/// </summary>
public class HeadcountResource
  : PlanningCenterSingletonFetchableResource<Headcount, HeadcountResource, CheckInsDocumentContext>,
  IIncludable<HeadcountResource, HeadcountIncludable>
{

  /// <summary>
  /// The related <see cref="AttendanceTypeResource" />.
  /// </summary>
  public AttendanceTypeResource AttendanceType => GetRelated<AttendanceTypeResource>("attendance_type");

  /// <summary>
  /// The related <see cref="EventTimeResource" />.
  /// </summary>
  public EventTimeResource EventTime => GetRelated<EventTimeResource>("event_time");

  internal HeadcountResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public HeadcountResource Include(params HeadcountIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of CheckIns Headcount resources.
/// </summary>
public class HeadcountResourceCollection
  : PlanningCenterPaginatedFetchableResource<Headcount, HeadcountResourceCollection, HeadcountResource, CheckInsDocumentContext>,
  IIncludable<HeadcountResourceCollection, HeadcountIncludable>,
  IOrderable<HeadcountResourceCollection, HeadcountOrderable>,
  IQueryable<HeadcountResourceCollection, HeadcountQueryable>
{
  internal HeadcountResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public HeadcountResourceCollection Include(params HeadcountIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public HeadcountResourceCollection OrderBy(HeadcountOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public HeadcountResourceCollection Query(params (HeadcountQueryable, string)[] queries)
    => base.Query(queries);
}

