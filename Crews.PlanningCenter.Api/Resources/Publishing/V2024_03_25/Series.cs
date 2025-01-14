/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Publishing.V2024_03_25.Entities;
using Crews.PlanningCenter.Models.Publishing.V2024_03_25.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Publishing.V2024_03_25;

/// <summary>
/// A fetchable Publishing Series resource.
/// </summary>
public class SeriesResource
  : PlanningCenterSingletonFetchableResource<Series, SeriesResource, PublishingDocumentContext>,
  IIncludable<SeriesResource, SeriesIncludable>
{

  /// <summary>
  /// The related <see cref="ChannelResource" />.
  /// </summary>
  public ChannelResource Channel => GetRelated<ChannelResource>("channel");

  internal SeriesResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public SeriesResource Include(params SeriesIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Publishing Series resources.
/// </summary>
public class SeriesResourceCollection
  : PlanningCenterPaginatedFetchableResource<Series, SeriesResourceCollection, SeriesResource, PublishingDocumentContext>,
  IIncludable<SeriesResourceCollection, SeriesIncludable>,
  IOrderable<SeriesResourceCollection, SeriesOrderable>,
  IQueryable<SeriesResourceCollection, SeriesQueryable>
{
  internal SeriesResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public SeriesResourceCollection Include(params SeriesIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public SeriesResourceCollection OrderBy(SeriesOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public SeriesResourceCollection Query(params (SeriesQueryable, string)[] queries)
    => base.Query(queries);
}

