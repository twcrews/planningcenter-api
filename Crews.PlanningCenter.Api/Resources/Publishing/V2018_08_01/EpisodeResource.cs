/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Publishing.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Publishing.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.Publishing.V2018_08_01;

/// <summary>
/// A fetchable Publishing EpisodeResource resource.
/// </summary>
public class EpisodeResourceResource
  : PlanningCenterSingletonFetchableResource<EpisodeResource, EpisodeResourceResource, PublishingDocumentContext>
{

  internal EpisodeResourceResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<EpisodeResource>> PostAsync(EpisodeResource resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<EpisodeResource>> PatchAsync(EpisodeResource resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Publishing EpisodeResource resources.
/// </summary>
public class EpisodeResourceResourceCollection
  : PlanningCenterPaginatedFetchableResource<EpisodeResource, EpisodeResourceResourceCollection, EpisodeResourceResource, PublishingDocumentContext>,
  IOrderable<EpisodeResourceResourceCollection, EpisodeResourceOrderable>
{
  internal EpisodeResourceResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public EpisodeResourceResourceCollection OrderBy(EpisodeResourceOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

