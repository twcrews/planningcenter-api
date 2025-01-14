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
/// A fetchable Services Song resource.
/// </summary>
public class SongResource
  : PlanningCenterSingletonFetchableResource<Song, SongResource, ServicesDocumentContext>
{

  /// <summary>
  /// The related <see cref="ArrangementResourceCollection" />.
  /// </summary>
  public ArrangementResourceCollection Arrangements => GetRelated<ArrangementResourceCollection>("arrangements");

  /// <summary>
  /// The related <see cref="AttachmentResourceCollection" />.
  /// </summary>
  public AttachmentResourceCollection Attachments => GetRelated<AttachmentResourceCollection>("attachments");

  /// <summary>
  /// The related <see cref="ItemResource" />.
  /// </summary>
  public ItemResource LastScheduledItem => GetRelated<ItemResource>("last_scheduled_item");

  /// <summary>
  /// The related <see cref="SongScheduleResourceCollection" />.
  /// </summary>
  public SongScheduleResourceCollection SongSchedules => GetRelated<SongScheduleResourceCollection>("song_schedules");

  /// <summary>
  /// The related <see cref="TagResourceCollection" />.
  /// </summary>
  public TagResourceCollection Tags => GetRelated<TagResourceCollection>("tags");

  internal SongResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Song>> PostAsync(Song resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Song>> PatchAsync(Song resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Services Song resources.
/// </summary>
public class SongResourceCollection
  : PlanningCenterPaginatedFetchableResource<Song, SongResourceCollection, SongResource, ServicesDocumentContext>,
  IOrderable<SongResourceCollection, SongOrderable>,
  IQueryable<SongResourceCollection, SongQueryable>
{
  internal SongResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public SongResourceCollection OrderBy(SongOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public SongResourceCollection Query(params (SongQueryable, string)[] queries)
    => base.Query(queries);
}

