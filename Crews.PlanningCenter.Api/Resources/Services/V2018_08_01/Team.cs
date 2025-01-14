/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services Team resource.
/// </summary>
public class TeamResource
  : PlanningCenterSingletonFetchableResource<Team, TeamResource, ServicesDocumentContext>,
  IIncludable<TeamResource, TeamIncludable>
{

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource People => GetRelated<PersonResource>("people");

  /// <summary>
  /// The related <see cref="PersonTeamPositionAssignmentResourceCollection" />.
  /// </summary>
  public PersonTeamPositionAssignmentResourceCollection PersonTeamPositionAssignments => GetRelated<PersonTeamPositionAssignmentResourceCollection>("person_team_position_assignments");

  /// <summary>
  /// The related <see cref="PlanPersonResource" />.
  /// </summary>
  public PlanPersonResource PlanPeople => GetRelated<PlanPersonResource>("plan_people");

  /// <summary>
  /// The related <see cref="ServiceTypeResource" />.
  /// </summary>
  public ServiceTypeResource ServiceType => GetRelated<ServiceTypeResource>("service_type");

  /// <summary>
  /// The related <see cref="TeamLeaderResourceCollection" />.
  /// </summary>
  public TeamLeaderResourceCollection TeamLeaders => GetRelated<TeamLeaderResourceCollection>("team_leaders");

  /// <summary>
  /// The related <see cref="TeamPositionResourceCollection" />.
  /// </summary>
  public TeamPositionResourceCollection TeamPositions => GetRelated<TeamPositionResourceCollection>("team_positions");

  internal TeamResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public TeamResource Include(params TeamIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Team>> PostAsync(Team resource)
    => base.PostAsync(resource);
}

/// <summary>
/// A fetchable collection of Services Team resources.
/// </summary>
public class TeamResourceCollection
  : PlanningCenterPaginatedFetchableResource<Team, TeamResourceCollection, TeamResource, ServicesDocumentContext>,
  IIncludable<TeamResourceCollection, TeamIncludable>,
  IOrderable<TeamResourceCollection, TeamOrderable>,
  IQueryable<TeamResourceCollection, TeamQueryable>
{
  internal TeamResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public TeamResourceCollection Include(params TeamIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public TeamResourceCollection OrderBy(TeamOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public TeamResourceCollection Query(params (TeamQueryable, string)[] queries)
    => base.Query(queries);
}

