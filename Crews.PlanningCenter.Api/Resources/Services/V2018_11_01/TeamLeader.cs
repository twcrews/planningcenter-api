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
/// A fetchable Services TeamLeader resource.
/// </summary>
public class TeamLeaderResource
  : PlanningCenterSingletonFetchableResource<TeamLeader, TeamLeaderResource, ServicesDocumentContext>,
  IIncludable<TeamLeaderResource, TeamLeaderIncludable>
{

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource People => GetRelated<PersonResource>("people");

  /// <summary>
  /// The related <see cref="TeamResource" />.
  /// </summary>
  public TeamResource Team => GetRelated<TeamResource>("team");

  internal TeamLeaderResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public TeamLeaderResource Include(params TeamLeaderIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Services TeamLeader resources.
/// </summary>
public class TeamLeaderResourceCollection
  : PlanningCenterPaginatedFetchableResource<TeamLeader, TeamLeaderResourceCollection, TeamLeaderResource, ServicesDocumentContext>,
  IIncludable<TeamLeaderResourceCollection, TeamLeaderIncludable>,
  IOrderable<TeamLeaderResourceCollection, TeamLeaderOrderable>
{
  internal TeamLeaderResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public TeamLeaderResourceCollection Include(params TeamLeaderIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public TeamLeaderResourceCollection OrderBy(TeamLeaderOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

