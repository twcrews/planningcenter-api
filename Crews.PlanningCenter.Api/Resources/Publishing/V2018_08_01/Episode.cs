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
/// A fetchable Publishing Episode resource.
/// </summary>
public class EpisodeResource
  : PlanningCenterSingletonFetchableResource<Episode, EpisodeResource, PublishingDocumentContext>,
  IIncludable<EpisodeResource, EpisodeIncludable>
{

  /// <summary>
  /// The related <see cref="ChannelResource" />.
  /// </summary>
  public ChannelResource Channel => GetRelated<ChannelResource>("channel");

  /// <summary>
  /// The related <see cref="EpisodeResourceResourceCollection" />.
  /// </summary>
  public EpisodeResourceResourceCollection EpisodeResources => GetRelated<EpisodeResourceResourceCollection>("episode_resources");

  /// <summary>
  /// The related <see cref="EpisodeTimeResourceCollection" />.
  /// </summary>
  public EpisodeTimeResourceCollection EpisodeTimes => GetRelated<EpisodeTimeResourceCollection>("episode_times");

  /// <summary>
  /// The related <see cref="NoteTemplateResource" />.
  /// </summary>
  public NoteTemplateResource NoteTemplate => GetRelated<NoteTemplateResource>("note_template");

  /// <summary>
  /// The related <see cref="SeriesResourceCollection" />.
  /// </summary>
  public SeriesResourceCollection Series => GetRelated<SeriesResourceCollection>("series");

  /// <summary>
  /// The related <see cref="SpeakershipResourceCollection" />.
  /// </summary>
  public SpeakershipResourceCollection Speakerships => GetRelated<SpeakershipResourceCollection>("speakerships");

  internal EpisodeResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public EpisodeResource Include(params EpisodeIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Episode>> PostAsync(Episode resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Episode>> PatchAsync(Episode resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Publishing Episode resources.
/// </summary>
public class EpisodeResourceCollection
  : PlanningCenterPaginatedFetchableResource<Episode, EpisodeResourceCollection, EpisodeResource, PublishingDocumentContext>,
  IIncludable<EpisodeResourceCollection, EpisodeIncludable>,
  IOrderable<EpisodeResourceCollection, EpisodeOrderable>,
  IQueryable<EpisodeResourceCollection, EpisodeQueryable>
{
  internal EpisodeResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public EpisodeResourceCollection Include(params EpisodeIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public EpisodeResourceCollection OrderBy(EpisodeOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public EpisodeResourceCollection Query(params (EpisodeQueryable, string)[] queries)
    => base.Query(queries);
}

