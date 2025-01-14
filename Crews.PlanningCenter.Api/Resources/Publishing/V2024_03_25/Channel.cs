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
/// A fetchable Publishing Channel resource.
/// </summary>
public class ChannelResource
  : PlanningCenterSingletonFetchableResource<Channel, ChannelResource, PublishingDocumentContext>,
  IIncludable<ChannelResource, ChannelIncludable>
{

  /// <summary>
  /// The related <see cref="ChannelDefaultEpisodeResourceResourceCollection" />.
  /// </summary>
  public ChannelDefaultEpisodeResourceResourceCollection ChannelDefaultEpisodeResources => GetRelated<ChannelDefaultEpisodeResourceResourceCollection>("channel_default_episode_resources");

  /// <summary>
  /// The related <see cref="ChannelDefaultTimeResourceCollection" />.
  /// </summary>
  public ChannelDefaultTimeResourceCollection ChannelDefaultTimes => GetRelated<ChannelDefaultTimeResourceCollection>("channel_default_times");

  /// <summary>
  /// The related <see cref="EpisodeResource" />.
  /// </summary>
  public EpisodeResource CurrentEpisode => GetRelated<EpisodeResource>("current_episode");

  /// <summary>
  /// The related <see cref="EpisodeResourceCollection" />.
  /// </summary>
  public EpisodeResourceCollection Episodes => GetRelated<EpisodeResourceCollection>("episodes");

  /// <summary>
  /// The related <see cref="ChannelNextTimeResourceCollection" />.
  /// </summary>
  public ChannelNextTimeResourceCollection NextTimes => GetRelated<ChannelNextTimeResourceCollection>("next_times");

  /// <summary>
  /// The related <see cref="SeriesResourceCollection" />.
  /// </summary>
  public SeriesResourceCollection Series => GetRelated<SeriesResourceCollection>("series");

  /// <summary>
  /// The related <see cref="EpisodeStatisticsResourceCollection" />.
  /// </summary>
  public EpisodeStatisticsResourceCollection Statistics => GetRelated<EpisodeStatisticsResourceCollection>("statistics");

  internal ChannelResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public ChannelResource Include(params ChannelIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Publishing Channel resources.
/// </summary>
public class ChannelResourceCollection
  : PlanningCenterPaginatedFetchableResource<Channel, ChannelResourceCollection, ChannelResource, PublishingDocumentContext>,
  IIncludable<ChannelResourceCollection, ChannelIncludable>,
  IOrderable<ChannelResourceCollection, ChannelOrderable>
{
  internal ChannelResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ChannelResourceCollection Include(params ChannelIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public ChannelResourceCollection OrderBy(ChannelOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

