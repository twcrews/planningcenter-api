/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Publishing.V2024_03_25.Entities;
using Crews.PlanningCenter.Models.Publishing.V2024_03_25.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Publishing.V2024_03_25;

/// <summary>
/// A fetchable Publishing EpisodeTime resource.
/// </summary>
public class EpisodeTimeResource
  : PlanningCenterSingletonFetchableResource<EpisodeTime, EpisodeTimeResource, PublishingDocumentContext>
{

  internal EpisodeTimeResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Publishing EpisodeTime resources.
/// </summary>
public class EpisodeTimeResourceCollection
  : PlanningCenterPaginatedFetchableResource<EpisodeTime, EpisodeTimeResourceCollection, EpisodeTimeResource, PublishingDocumentContext>,
  IOrderable<EpisodeTimeResourceCollection, EpisodeTimeOrderable>
{
  internal EpisodeTimeResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public EpisodeTimeResourceCollection OrderBy(EpisodeTimeOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

