/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2024_11_07.Entities;
using Crews.PlanningCenter.Models.CheckIns.V2024_11_07.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2024_11_07;

/// <summary>
/// A fetchable CheckIns AttendanceType resource.
/// </summary>
public class AttendanceTypeResource
  : PlanningCenterSingletonFetchableResource<AttendanceType, AttendanceTypeResource, CheckInsDocumentContext>,
  IIncludable<AttendanceTypeResource, AttendanceTypeIncludable>
{

  /// <summary>
  /// The related <see cref="EventResource" />.
  /// </summary>
  public EventResource Event => GetRelated<EventResource>("event");

  /// <summary>
  /// The related <see cref="HeadcountResourceCollection" />.
  /// </summary>
  public HeadcountResourceCollection Headcounts => GetRelated<HeadcountResourceCollection>("headcounts");

  internal AttendanceTypeResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public AttendanceTypeResource Include(params AttendanceTypeIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of CheckIns AttendanceType resources.
/// </summary>
public class AttendanceTypeResourceCollection
  : PlanningCenterPaginatedFetchableResource<AttendanceType, AttendanceTypeResourceCollection, AttendanceTypeResource, CheckInsDocumentContext>,
  IIncludable<AttendanceTypeResourceCollection, AttendanceTypeIncludable>,
  IQueryable<AttendanceTypeResourceCollection, AttendanceTypeQueryable>
{
  internal AttendanceTypeResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public AttendanceTypeResourceCollection Include(params AttendanceTypeIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public AttendanceTypeResourceCollection Query(params KeyValuePair<AttendanceTypeQueryable, string>[] queries)
    => base.Query(queries);
}

