/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.CheckIns.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2018_08_01;

/// <summary>
/// A fetchable CheckIns LocationLabel resource.
/// </summary>
public class LocationLabelResource
  : PlanningCenterSingletonFetchableResource<LocationLabel, LocationLabelResource, CheckInsDocumentContext>,
  IIncludable<LocationLabelResource, LocationLabelIncludable>
{

  /// <summary>
  /// The related <see cref="LabelResource" />.
  /// </summary>
  public LabelResource Label => GetRelated<LabelResource>("label");

  /// <summary>
  /// The related <see cref="LocationResource" />.
  /// </summary>
  public LocationResource Location => GetRelated<LocationResource>("location");

  internal LocationLabelResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public LocationLabelResource Include(params LocationLabelIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of CheckIns LocationLabel resources.
/// </summary>
public class LocationLabelResourceCollection
  : PlanningCenterPaginatedFetchableResource<LocationLabel, LocationLabelResourceCollection, LocationLabelResource, CheckInsDocumentContext>,
  IIncludable<LocationLabelResourceCollection, LocationLabelIncludable>
{
  internal LocationLabelResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public LocationLabelResourceCollection Include(params LocationLabelIncludable[] included)
    => base.Include(included);
}

