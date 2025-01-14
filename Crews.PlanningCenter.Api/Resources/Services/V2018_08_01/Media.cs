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
/// A fetchable Services Media resource.
/// </summary>
public class MediaResource
  : PlanningCenterSingletonFetchableResource<Media, MediaResource, ServicesDocumentContext>,
  IIncludable<MediaResource, MediaIncludable>
{

  /// <summary>
  /// The related <see cref="AttachmentResourceCollection" />.
  /// </summary>
  public AttachmentResourceCollection Attachments => GetRelated<AttachmentResourceCollection>("attachments");

  /// <summary>
  /// The related <see cref="MediaScheduleResourceCollection" />.
  /// </summary>
  public MediaScheduleResourceCollection MediaSchedules => GetRelated<MediaScheduleResourceCollection>("media_schedules");

  /// <summary>
  /// The related <see cref="TagResourceCollection" />.
  /// </summary>
  public TagResourceCollection Tags => GetRelated<TagResourceCollection>("tags");

  internal MediaResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public MediaResource Include(params MediaIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Media>> PostAsync(Media resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Media>> PatchAsync(Media resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Services Media resources.
/// </summary>
public class MediaResourceCollection
  : PlanningCenterPaginatedFetchableResource<Media, MediaResourceCollection, MediaResource, ServicesDocumentContext>,
  IIncludable<MediaResourceCollection, MediaIncludable>,
  IOrderable<MediaResourceCollection, MediaOrderable>,
  IQueryable<MediaResourceCollection, MediaQueryable>
{
  internal MediaResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public MediaResourceCollection Include(params MediaIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public MediaResourceCollection OrderBy(MediaOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public MediaResourceCollection Query(params (MediaQueryable, string)[] queries)
    => base.Query(queries);
}

