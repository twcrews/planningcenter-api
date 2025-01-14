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
/// A fetchable Publishing EpisodeResource resource.
/// </summary>
public class EpisodeResourceResource
  : PlanningCenterSingletonFetchableResource<EpisodeResource, EpisodeResourceResource, PublishingDocumentContext>
{

  internal EpisodeResourceResource(Uri uri, HttpClient client) : base(uri, client) { }
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

