/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services ItemNote resource.
/// </summary>
public class ItemNoteResource
  : PlanningCenterSingletonFetchableResource<ItemNote, ItemNoteResource, ServicesDocumentContext>,
  IIncludable<ItemNoteResource, ItemNoteIncludable>
{

  /// <summary>
  /// The related <see cref="ItemNoteCategoryResource" />.
  /// </summary>
  public ItemNoteCategoryResource ItemNoteCategory => GetRelated<ItemNoteCategoryResource>("item_note_category");

  internal ItemNoteResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public ItemNoteResource Include(params ItemNoteIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Services ItemNote resources.
/// </summary>
public class ItemNoteResourceCollection
  : PlanningCenterPaginatedFetchableResource<ItemNote, ItemNoteResourceCollection, ItemNoteResource, ServicesDocumentContext>,
  IIncludable<ItemNoteResourceCollection, ItemNoteIncludable>
{
  internal ItemNoteResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ItemNoteResourceCollection Include(params ItemNoteIncludable[] included)
    => base.Include(included);
}

