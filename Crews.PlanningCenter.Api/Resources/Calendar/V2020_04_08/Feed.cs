/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2020_04_08.Entities;
using Crews.PlanningCenter.Models.Calendar.V2020_04_08.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2020_04_08;

/// <summary>
/// A fetchable Calendar Feed resource.
/// </summary>
public class FeedResource
  : PlanningCenterSingletonFetchableResource<Feed, FeedResource, CalendarDocumentContext>
{

  internal FeedResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Calendar Feed resources.
/// </summary>
public class FeedResourceCollection
  : PlanningCenterPaginatedFetchableResource<Feed, FeedResourceCollection, FeedResource, CalendarDocumentContext>,
  IOrderable<FeedResourceCollection, FeedOrderable>,
  IQueryable<FeedResourceCollection, FeedQueryable>
{
  internal FeedResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public FeedResourceCollection OrderBy(FeedOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public FeedResourceCollection Query(params KeyValuePair<FeedQueryable, string>[] queries)
    => base.Query(queries);
}

