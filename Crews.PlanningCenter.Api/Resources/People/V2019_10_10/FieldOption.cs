/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2019_10_10.Entities;
using Crews.PlanningCenter.Models.People.V2019_10_10.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2019_10_10;

/// <summary>
/// A fetchable People FieldOption resource.
/// </summary>
public class FieldOptionResource
  : PlanningCenterSingletonFetchableResource<FieldOption, FieldOptionResource, PeopleDocumentContext>
{

  internal FieldOptionResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People FieldOption resources.
/// </summary>
public class FieldOptionResourceCollection
  : PlanningCenterPaginatedFetchableResource<FieldOption, FieldOptionResourceCollection, FieldOptionResource, PeopleDocumentContext>,
  IOrderable<FieldOptionResourceCollection, FieldOptionOrderable>,
  IQueryable<FieldOptionResourceCollection, FieldOptionQueryable>
{
  internal FieldOptionResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public FieldOptionResourceCollection OrderBy(FieldOptionOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public FieldOptionResourceCollection Query(params KeyValuePair<FieldOptionQueryable, string>[] queries)
    => base.Query(queries);
}

