/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2022_07_14.Entities;
using Crews.PlanningCenter.Models.People.V2022_07_14.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2022_07_14;

/// <summary>
/// A fetchable People MaritalStatus resource.
/// </summary>
public class MaritalStatusResource
  : PlanningCenterSingletonFetchableResource<MaritalStatus, MaritalStatusResource, PeopleDocumentContext>
{

  internal MaritalStatusResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People MaritalStatus resources.
/// </summary>
public class MaritalStatusResourceCollection
  : PlanningCenterPaginatedFetchableResource<MaritalStatus, MaritalStatusResourceCollection, MaritalStatusResource, PeopleDocumentContext>,
  IOrderable<MaritalStatusResourceCollection, MaritalStatusOrderable>,
  IQueryable<MaritalStatusResourceCollection, MaritalStatusQueryable>
{
  internal MaritalStatusResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public MaritalStatusResourceCollection OrderBy(MaritalStatusOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public MaritalStatusResourceCollection Query(params KeyValuePair<MaritalStatusQueryable, string>[] queries)
    => base.Query(queries);
}

