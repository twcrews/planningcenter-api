/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Giving.V2019_10_18.Entities;
using Crews.PlanningCenter.Models.Giving.V2019_10_18.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.Giving.V2019_10_18;

/// <summary>
/// A fetchable Giving Fund resource.
/// </summary>
public class FundResource
  : PlanningCenterSingletonFetchableResource<Fund, FundResource, GivingDocumentContext>
{

  internal FundResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Fund>> PostAsync(Fund resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Fund>> PatchAsync(Fund resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Giving Fund resources.
/// </summary>
public class FundResourceCollection
  : PlanningCenterPaginatedFetchableResource<Fund, FundResourceCollection, FundResource, GivingDocumentContext>,
  IQueryable<FundResourceCollection, FundQueryable>
{
  internal FundResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public FundResourceCollection Query(params (FundQueryable, string)[] queries)
    => base.Query(queries);
}

