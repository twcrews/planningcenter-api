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
/// A fetchable Services PersonTeamPositionAssignment resource.
/// </summary>
public class PersonTeamPositionAssignmentResource
  : PlanningCenterSingletonFetchableResource<PersonTeamPositionAssignment, PersonTeamPositionAssignmentResource, ServicesDocumentContext>,
  IIncludable<PersonTeamPositionAssignmentResource, PersonTeamPositionAssignmentIncludable>
{

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  /// <summary>
  /// The related <see cref="TeamPositionResource" />.
  /// </summary>
  public TeamPositionResource TeamPosition => GetRelated<TeamPositionResource>("team_position");

  internal PersonTeamPositionAssignmentResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public PersonTeamPositionAssignmentResource Include(params PersonTeamPositionAssignmentIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Services PersonTeamPositionAssignment resources.
/// </summary>
public class PersonTeamPositionAssignmentResourceCollection
  : PlanningCenterPaginatedFetchableResource<PersonTeamPositionAssignment, PersonTeamPositionAssignmentResourceCollection, PersonTeamPositionAssignmentResource, ServicesDocumentContext>,
  IIncludable<PersonTeamPositionAssignmentResourceCollection, PersonTeamPositionAssignmentIncludable>,
  IOrderable<PersonTeamPositionAssignmentResourceCollection, PersonTeamPositionAssignmentOrderable>
{
  internal PersonTeamPositionAssignmentResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PersonTeamPositionAssignmentResourceCollection Include(params PersonTeamPositionAssignmentIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public PersonTeamPositionAssignmentResourceCollection OrderBy(PersonTeamPositionAssignmentOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

