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
/// A fetchable Services PlanPerson resource.
/// </summary>
public class PlanPersonResource
  : PlanningCenterSingletonFetchableResource<PlanPerson, PlanPersonResource, ServicesDocumentContext>,
  IIncludable<PlanPersonResource, PlanPersonIncludable>
{

  /// <summary>
  /// The related <see cref="PlanTimeResourceCollection" />.
  /// </summary>
  public PlanTimeResourceCollection DeclinedPlanTimes => GetRelated<PlanTimeResourceCollection>("declined_plan_times");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  /// <summary>
  /// The related <see cref="PlanResource" />.
  /// </summary>
  public PlanResource Plan => GetRelated<PlanResource>("plan");

  /// <summary>
  /// The related <see cref="PlanPersonTimeResourceCollection" />.
  /// </summary>
  public PlanPersonTimeResourceCollection PlanPersonTimes => GetRelated<PlanPersonTimeResourceCollection>("plan_person_times");

  /// <summary>
  /// The related <see cref="PlanTimeResourceCollection" />.
  /// </summary>
  public PlanTimeResourceCollection PlanTimes => GetRelated<PlanTimeResourceCollection>("plan_times");

  /// <summary>
  /// The related <see cref="TeamResource" />.
  /// </summary>
  public TeamResource Team => GetRelated<TeamResource>("team");

  internal PlanPersonResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public PlanPersonResource Include(params PlanPersonIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<PlanPerson>> PostAsync(PlanPerson resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<PlanPerson>> PatchAsync(PlanPerson resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Services PlanPerson resources.
/// </summary>
public class PlanPersonResourceCollection
  : PlanningCenterPaginatedFetchableResource<PlanPerson, PlanPersonResourceCollection, PlanPersonResource, ServicesDocumentContext>,
  IIncludable<PlanPersonResourceCollection, PlanPersonIncludable>,
  IQueryable<PlanPersonResourceCollection, PlanPersonQueryable>
{
  internal PlanPersonResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PlanPersonResourceCollection Include(params PlanPersonIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public PlanPersonResourceCollection Query(params (PlanPersonQueryable, string)[] queries)
    => base.Query(queries);
}

