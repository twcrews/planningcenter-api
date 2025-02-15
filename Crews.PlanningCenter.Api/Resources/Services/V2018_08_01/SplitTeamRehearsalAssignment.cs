/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services SplitTeamRehearsalAssignment resource.
/// </summary>
public class SplitTeamRehearsalAssignmentResource
  : PlanningCenterSingletonFetchableResource<SplitTeamRehearsalAssignment, SplitTeamRehearsalAssignmentResource, ServicesDocumentContext>
{

  /// <summary>
  /// The related <see cref="TeamResource" />.
  /// </summary>
  public TeamResource Team => GetRelated<TeamResource>("team");

  internal SplitTeamRehearsalAssignmentResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<SplitTeamRehearsalAssignment>> PostAsync(SplitTeamRehearsalAssignment resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<SplitTeamRehearsalAssignment>> PatchAsync(SplitTeamRehearsalAssignment resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Services SplitTeamRehearsalAssignment resources.
/// </summary>
public class SplitTeamRehearsalAssignmentResourceCollection
  : PlanningCenterPaginatedFetchableResource<SplitTeamRehearsalAssignment, SplitTeamRehearsalAssignmentResourceCollection, SplitTeamRehearsalAssignmentResource, ServicesDocumentContext>
{
  internal SplitTeamRehearsalAssignmentResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

