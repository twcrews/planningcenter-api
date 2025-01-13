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
/// A fetchable Groups Location resource.
/// </summary>
public class LocationResource
  : PlanningCenterSingletonFetchableResource<Location, LocationResource, GroupsDocumentContext>,
  IIncludable<LocationResource, LocationIncludable>
{

  /// <summary>
  /// The related <see cref="GroupResource" />.
  /// </summary>
  public GroupResource Group => GetRelated<GroupResource>("group");

  internal LocationResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public LocationResource Include(params LocationIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Groups Location resources.
/// </summary>
public class LocationResourceCollection
  : PlanningCenterPaginatedFetchableResource<Location, LocationResourceCollection, LocationResource, GroupsDocumentContext>,
  IIncludable<LocationResourceCollection, LocationIncludable>
{
  internal LocationResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public LocationResourceCollection Include(params LocationIncludable[] included)
    => base.Include(included);
}

