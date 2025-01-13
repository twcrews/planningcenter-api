/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2022_01_05.Entities;
using Crews.PlanningCenter.Models.People.V2022_01_05.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2022_01_05;

/// <summary>
/// A fetchable People Note resource.
/// </summary>
public class NoteResource
  : PlanningCenterSingletonFetchableResource<Note, NoteResource, PeopleDocumentContext>,
  IIncludable<NoteResource, NoteIncludable>
{

  /// <summary>
  /// The related <see cref="NoteCategoryResource" />.
  /// </summary>
  public NoteCategoryResource Category => GetRelated<NoteCategoryResource>("category");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource CreatedBy => GetRelated<PersonResource>("created_by");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal NoteResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public NoteResource Include(params NoteIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People Note resources.
/// </summary>
public class NoteResourceCollection
  : PlanningCenterPaginatedFetchableResource<Note, NoteResourceCollection, NoteResource, PeopleDocumentContext>,
  IIncludable<NoteResourceCollection, NoteIncludable>,
  IOrderable<NoteResourceCollection, NoteOrderable>,
  IQueryable<NoteResourceCollection, NoteQueryable>
{
  internal NoteResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public NoteResourceCollection Include(params NoteIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public NoteResourceCollection OrderBy(NoteOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public NoteResourceCollection Query(params KeyValuePair<NoteQueryable, string>[] queries)
    => base.Query(queries);
}

