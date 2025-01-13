/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Groups.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Groups.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Groups.V2018_08_01;

/// <summary>
/// A fetchable Groups Attendance resource.
/// </summary>
public class AttendanceResource
  : PlanningCenterSingletonFetchableResource<Attendance, AttendanceResource, GroupsDocumentContext>,
  IIncludable<AttendanceResource, AttendanceIncludable>
{

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal AttendanceResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public AttendanceResource Include(params AttendanceIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Groups Attendance resources.
/// </summary>
public class AttendanceResourceCollection
  : PlanningCenterPaginatedFetchableResource<Attendance, AttendanceResourceCollection, AttendanceResource, GroupsDocumentContext>,
  IIncludable<AttendanceResourceCollection, AttendanceIncludable>,
  IOrderable<AttendanceResourceCollection, AttendanceOrderable>,
  IQueryable<AttendanceResourceCollection, AttendanceQueryable>
{
  internal AttendanceResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public AttendanceResourceCollection Include(params AttendanceIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public AttendanceResourceCollection OrderBy(AttendanceOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public AttendanceResourceCollection Query(params KeyValuePair<AttendanceQueryable, string>[] queries)
    => base.Query(queries);
}

