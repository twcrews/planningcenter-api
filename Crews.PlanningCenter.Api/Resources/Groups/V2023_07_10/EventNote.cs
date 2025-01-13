/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Groups.V2023_07_10.Entities;
using Crews.PlanningCenter.Models.Groups.V2023_07_10.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Groups.V2023_07_10;

/// <summary>
/// A fetchable Groups EventNote resource.
/// </summary>
public class EventNoteResource
  : PlanningCenterSingletonFetchableResource<EventNote, EventNoteResource, GroupsDocumentContext>,
  IIncludable<EventNoteResource, EventNoteIncludable>
{

  /// <summary>
  /// The related <see cref="OwnerResource" />.
  /// </summary>
  public OwnerResource Owner => GetRelated<OwnerResource>("owner");

  internal EventNoteResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public EventNoteResource Include(params EventNoteIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Groups EventNote resources.
/// </summary>
public class EventNoteResourceCollection
  : PlanningCenterPaginatedFetchableResource<EventNote, EventNoteResourceCollection, EventNoteResource, GroupsDocumentContext>,
  IIncludable<EventNoteResourceCollection, EventNoteIncludable>
{
  internal EventNoteResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public EventNoteResourceCollection Include(params EventNoteIncludable[] included)
    => base.Include(included);
}

