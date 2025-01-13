/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2020_04_06.Entities;
using Crews.PlanningCenter.Models.People.V2020_04_06.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2020_04_06;

/// <summary>
/// A fetchable People InactiveReason resource.
/// </summary>
public class InactiveReasonResource
  : PlanningCenterSingletonFetchableResource<InactiveReason, InactiveReasonResource, PeopleDocumentContext>
{

  internal InactiveReasonResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People InactiveReason resources.
/// </summary>
public class InactiveReasonResourceCollection
  : PlanningCenterPaginatedFetchableResource<InactiveReason, InactiveReasonResourceCollection, InactiveReasonResource, PeopleDocumentContext>,
  IOrderable<InactiveReasonResourceCollection, InactiveReasonOrderable>,
  IQueryable<InactiveReasonResourceCollection, InactiveReasonQueryable>
{
  internal InactiveReasonResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public InactiveReasonResourceCollection OrderBy(InactiveReasonOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public InactiveReasonResourceCollection Query(params KeyValuePair<InactiveReasonQueryable, string>[] queries)
    => base.Query(queries);
}

