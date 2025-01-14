/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2022_01_28.Entities;
using Crews.PlanningCenter.Models.People.V2022_01_28.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2022_01_28;

/// <summary>
/// A fetchable People NoteCategorySubscription resource.
/// </summary>
public class NoteCategorySubscriptionResource
  : PlanningCenterSingletonFetchableResource<NoteCategorySubscription, NoteCategorySubscriptionResource, PeopleDocumentContext>,
  IIncludable<NoteCategorySubscriptionResource, NoteCategorySubscriptionIncludable>
{

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal NoteCategorySubscriptionResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public NoteCategorySubscriptionResource Include(params NoteCategorySubscriptionIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People NoteCategorySubscription resources.
/// </summary>
public class NoteCategorySubscriptionResourceCollection
  : PlanningCenterPaginatedFetchableResource<NoteCategorySubscription, NoteCategorySubscriptionResourceCollection, NoteCategorySubscriptionResource, PeopleDocumentContext>,
  IIncludable<NoteCategorySubscriptionResourceCollection, NoteCategorySubscriptionIncludable>,
  IOrderable<NoteCategorySubscriptionResourceCollection, NoteCategorySubscriptionOrderable>,
  IQueryable<NoteCategorySubscriptionResourceCollection, NoteCategorySubscriptionQueryable>
{
  internal NoteCategorySubscriptionResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public NoteCategorySubscriptionResourceCollection Include(params NoteCategorySubscriptionIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public NoteCategorySubscriptionResourceCollection OrderBy(NoteCategorySubscriptionOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public NoteCategorySubscriptionResourceCollection Query(params (NoteCategorySubscriptionQueryable, string)[] queries)
    => base.Query(queries);
}

