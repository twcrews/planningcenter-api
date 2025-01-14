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
/// A fetchable Services Item resource.
/// </summary>
public class ItemResource
  : PlanningCenterSingletonFetchableResource<Item, ItemResource, ServicesDocumentContext>,
  IIncludable<ItemResource, ItemIncludable>
{

  /// <summary>
  /// The related <see cref="ArrangementResource" />.
  /// </summary>
  public ArrangementResource Arrangement => GetRelated<ArrangementResource>("arrangement");

  /// <summary>
  /// The related <see cref="AttachmentResourceCollection" />.
  /// </summary>
  public AttachmentResourceCollection Attachments => GetRelated<AttachmentResourceCollection>("attachments");

  /// <summary>
  /// The related <see cref="CustomSlideResourceCollection" />.
  /// </summary>
  public CustomSlideResourceCollection CustomSlides => GetRelated<CustomSlideResourceCollection>("custom_slides");

  /// <summary>
  /// The related <see cref="ItemNoteResourceCollection" />.
  /// </summary>
  public ItemNoteResourceCollection ItemNotes => GetRelated<ItemNoteResourceCollection>("item_notes");

  /// <summary>
  /// The related <see cref="ItemTimeResourceCollection" />.
  /// </summary>
  public ItemTimeResourceCollection ItemTimes => GetRelated<ItemTimeResourceCollection>("item_times");

  /// <summary>
  /// The related <see cref="KeyResource" />.
  /// </summary>
  public KeyResource Key => GetRelated<KeyResource>("key");

  /// <summary>
  /// The related <see cref="MediaResource" />.
  /// </summary>
  public MediaResource Media => GetRelated<MediaResource>("media");

  /// <summary>
  /// The related <see cref="AttachmentResource" />.
  /// </summary>
  public AttachmentResource SelectedAttachment => GetRelated<AttachmentResource>("selected_attachment");

  /// <summary>
  /// The related <see cref="AttachmentResource" />.
  /// </summary>
  public AttachmentResource SelectedBackground => GetRelated<AttachmentResource>("selected_background");

  /// <summary>
  /// The related <see cref="SongResource" />.
  /// </summary>
  public SongResource Song => GetRelated<SongResource>("song");

  internal ItemResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public ItemResource Include(params ItemIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Item>> PostAsync(Item resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Item>> PatchAsync(Item resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Services Item resources.
/// </summary>
public class ItemResourceCollection
  : PlanningCenterPaginatedFetchableResource<Item, ItemResourceCollection, ItemResource, ServicesDocumentContext>,
  IIncludable<ItemResourceCollection, ItemIncludable>
{
  internal ItemResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ItemResourceCollection Include(params ItemIncludable[] included)
    => base.Include(included);
}

