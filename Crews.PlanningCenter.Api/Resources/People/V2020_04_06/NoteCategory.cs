/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2020_04_06.Entities;
using Crews.PlanningCenter.Models.People.V2020_04_06.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2020_04_06;

/// <summary>
/// A fetchable People NoteCategory resource.
/// </summary>
public class NoteCategoryResource
  : PlanningCenterSingletonFetchableResource<NoteCategory, NoteCategoryResource, PeopleDocumentContext>,
  IIncludable<NoteCategoryResource, NoteCategoryIncludable>
{

  /// <summary>
  /// The related <see cref="NoteCategoryShareResourceCollection" />.
  /// </summary>
  public NoteCategoryShareResourceCollection Shares => GetRelated<NoteCategoryShareResourceCollection>("shares");

  /// <summary>
  /// The related <see cref="PersonResourceCollection" />.
  /// </summary>
  public PersonResourceCollection Subscribers => GetRelated<PersonResourceCollection>("subscribers");

  /// <summary>
  /// The related <see cref="NoteCategorySubscriptionResourceCollection" />.
  /// </summary>
  public NoteCategorySubscriptionResourceCollection Subscriptions => GetRelated<NoteCategorySubscriptionResourceCollection>("subscriptions");

  internal NoteCategoryResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public NoteCategoryResource Include(params NoteCategoryIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<NoteCategory>> PostAsync(NoteCategory resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<NoteCategory>> PatchAsync(NoteCategory resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People NoteCategory resources.
/// </summary>
public class NoteCategoryResourceCollection
  : PlanningCenterPaginatedFetchableResource<NoteCategory, NoteCategoryResourceCollection, NoteCategoryResource, PeopleDocumentContext>,
  IIncludable<NoteCategoryResourceCollection, NoteCategoryIncludable>,
  IOrderable<NoteCategoryResourceCollection, NoteCategoryOrderable>,
  IQueryable<NoteCategoryResourceCollection, NoteCategoryQueryable>
{
  internal NoteCategoryResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public NoteCategoryResourceCollection Include(params NoteCategoryIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public NoteCategoryResourceCollection OrderBy(NoteCategoryOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public NoteCategoryResourceCollection Query(params (NoteCategoryQueryable, string)[] queries)
    => base.Query(queries);
}

