/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Giving.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Giving.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.Giving.V2018_08_01;

/// <summary>
/// A fetchable Giving Label resource.
/// </summary>
public class LabelResource
  : PlanningCenterSingletonFetchableResource<Label, LabelResource, GivingDocumentContext>
{

  internal LabelResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Label>> PostAsync(Label resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Label>> PatchAsync(Label resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Giving Label resources.
/// </summary>
public class LabelResourceCollection
  : PlanningCenterPaginatedFetchableResource<Label, LabelResourceCollection, LabelResource, GivingDocumentContext>,
  IQueryable<LabelResourceCollection, LabelQueryable>
{
  internal LabelResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public LabelResourceCollection Query(params (LabelQueryable, string)[] queries)
    => base.Query(queries);
}

