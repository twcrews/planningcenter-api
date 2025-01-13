/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2020_07_22.Entities;
using Crews.PlanningCenter.Models.People.V2020_07_22.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2020_07_22;

/// <summary>
/// A fetchable People NameSuffix resource.
/// </summary>
public class NameSuffixResource
  : PlanningCenterSingletonFetchableResource<NameSuffix, NameSuffixResource, PeopleDocumentContext>
{

  internal NameSuffixResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People NameSuffix resources.
/// </summary>
public class NameSuffixResourceCollection
  : PlanningCenterPaginatedFetchableResource<NameSuffix, NameSuffixResourceCollection, NameSuffixResource, PeopleDocumentContext>,
  IOrderable<NameSuffixResourceCollection, NameSuffixOrderable>,
  IQueryable<NameSuffixResourceCollection, NameSuffixQueryable>
{
  internal NameSuffixResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public NameSuffixResourceCollection OrderBy(NameSuffixOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public NameSuffixResourceCollection Query(params KeyValuePair<NameSuffixQueryable, string>[] queries)
    => base.Query(queries);
}

