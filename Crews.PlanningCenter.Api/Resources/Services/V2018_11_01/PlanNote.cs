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
/// A fetchable Services PlanNote resource.
/// </summary>
public class PlanNoteResource
  : PlanningCenterSingletonFetchableResource<PlanNote, PlanNoteResource, ServicesDocumentContext>,
  IIncludable<PlanNoteResource, PlanNoteIncludable>
{

  /// <summary>
  /// The related <see cref="PlanNoteCategoryResource" />.
  /// </summary>
  public PlanNoteCategoryResource PlanNoteCategory => GetRelated<PlanNoteCategoryResource>("plan_note_category");

  internal PlanNoteResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public PlanNoteResource Include(params PlanNoteIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<PlanNote>> PostAsync(PlanNote resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<PlanNote>> PatchAsync(PlanNote resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Services PlanNote resources.
/// </summary>
public class PlanNoteResourceCollection
  : PlanningCenterPaginatedFetchableResource<PlanNote, PlanNoteResourceCollection, PlanNoteResource, ServicesDocumentContext>,
  IIncludable<PlanNoteResourceCollection, PlanNoteIncludable>,
  IOrderable<PlanNoteResourceCollection, PlanNoteOrderable>,
  IQueryable<PlanNoteResourceCollection, PlanNoteQueryable>
{
  internal PlanNoteResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PlanNoteResourceCollection Include(params PlanNoteIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public PlanNoteResourceCollection OrderBy(PlanNoteOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public PlanNoteResourceCollection Query(params (PlanNoteQueryable, string)[] queries)
    => base.Query(queries);
}

