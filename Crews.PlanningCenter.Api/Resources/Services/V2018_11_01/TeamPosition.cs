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
/// A fetchable Services TeamPosition resource.
/// </summary>
public class TeamPositionResource
  : PlanningCenterSingletonFetchableResource<TeamPosition, TeamPositionResource, ServicesDocumentContext>,
  IIncludable<TeamPositionResource, TeamPositionIncludable>
{

  /// <summary>
  /// The related <see cref="PersonTeamPositionAssignmentResourceCollection" />.
  /// </summary>
  public PersonTeamPositionAssignmentResourceCollection PersonTeamPositionAssignments => GetRelated<PersonTeamPositionAssignmentResourceCollection>("person_team_position_assignments");

  /// <summary>
  /// The related <see cref="TagResourceCollection" />.
  /// </summary>
  public TagResourceCollection Tags => GetRelated<TagResourceCollection>("tags");

  /// <summary>
  /// The related <see cref="TeamResource" />.
  /// </summary>
  public TeamResource Team => GetRelated<TeamResource>("team");

  internal TeamPositionResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public TeamPositionResource Include(params TeamPositionIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Services TeamPosition resources.
/// </summary>
public class TeamPositionResourceCollection
  : PlanningCenterPaginatedFetchableResource<TeamPosition, TeamPositionResourceCollection, TeamPositionResource, ServicesDocumentContext>,
  IIncludable<TeamPositionResourceCollection, TeamPositionIncludable>,
  IOrderable<TeamPositionResourceCollection, TeamPositionOrderable>
{
  internal TeamPositionResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public TeamPositionResourceCollection Include(params TeamPositionIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public TeamPositionResourceCollection OrderBy(TeamPositionOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

