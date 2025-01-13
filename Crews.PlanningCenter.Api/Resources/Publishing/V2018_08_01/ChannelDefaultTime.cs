/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Publishing.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Publishing.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Publishing.V2018_08_01;

/// <summary>
/// A fetchable Publishing ChannelDefaultTime resource.
/// </summary>
public class ChannelDefaultTimeResource
  : PlanningCenterSingletonFetchableResource<ChannelDefaultTime, ChannelDefaultTimeResource, PublishingDocumentContext>
{

  internal ChannelDefaultTimeResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Publishing ChannelDefaultTime resources.
/// </summary>
public class ChannelDefaultTimeResourceCollection
  : PlanningCenterPaginatedFetchableResource<ChannelDefaultTime, ChannelDefaultTimeResourceCollection, ChannelDefaultTimeResource, PublishingDocumentContext>,
  IOrderable<ChannelDefaultTimeResourceCollection, ChannelDefaultTimeOrderable>
{
  internal ChannelDefaultTimeResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ChannelDefaultTimeResourceCollection OrderBy(ChannelDefaultTimeOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

