/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_11_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_11_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_11_01;

/// <summary>
/// A fetchable Services PlanTime resource.
/// </summary>
public class PlanTimeResource
  : PlanningCenterSingletonFetchableResource<PlanTime, PlanTimeResource, ServicesDocumentContext>,
  IIncludable<PlanTimeResource, PlanTimeIncludable>
{

  /// <summary>
  /// The related <see cref="SplitTeamRehearsalAssignmentResourceCollection" />.
  /// </summary>
  public SplitTeamRehearsalAssignmentResourceCollection SplitTeamRehearsalAssignments => GetRelated<SplitTeamRehearsalAssignmentResourceCollection>("split_team_rehearsal_assignments");

  internal PlanTimeResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public PlanTimeResource Include(params PlanTimeIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<PlanTime>> PostAsync(PlanTime resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<PlanTime>> PatchAsync(PlanTime resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Services PlanTime resources.
/// </summary>
public class PlanTimeResourceCollection
  : PlanningCenterPaginatedFetchableResource<PlanTime, PlanTimeResourceCollection, PlanTimeResource, ServicesDocumentContext>,
  IIncludable<PlanTimeResourceCollection, PlanTimeIncludable>,
  IOrderable<PlanTimeResourceCollection, PlanTimeOrderable>,
  IQueryable<PlanTimeResourceCollection, PlanTimeQueryable>
{
  internal PlanTimeResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PlanTimeResourceCollection Include(params PlanTimeIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public PlanTimeResourceCollection OrderBy(PlanTimeOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public PlanTimeResourceCollection Query(params (PlanTimeQueryable, string)[] queries)
    => base.Query(queries);
}

