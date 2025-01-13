/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_11_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_11_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_11_01;

/// <summary>
/// A fetchable Services Series resource.
/// </summary>
public class SeriesResource
  : PlanningCenterSingletonFetchableResource<Series, SeriesResource, ServicesDocumentContext>
{

  /// <summary>
  /// The related <see cref="PlanResourceCollection" />.
  /// </summary>
  public PlanResourceCollection Plans => GetRelated<PlanResourceCollection>("plans");

  internal SeriesResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services Series resources.
/// </summary>
public class SeriesResourceCollection
  : PlanningCenterPaginatedFetchableResource<Series, SeriesResourceCollection, SeriesResource, ServicesDocumentContext>,
  IOrderable<SeriesResourceCollection, SeriesOrderable>,
  IQueryable<SeriesResourceCollection, SeriesQueryable>
{
  internal SeriesResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public SeriesResourceCollection OrderBy(SeriesOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public SeriesResourceCollection Query(params KeyValuePair<SeriesQueryable, string>[] queries)
    => base.Query(queries);
}

