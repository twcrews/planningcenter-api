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
/// A fetchable People NamePrefix resource.
/// </summary>
public class NamePrefixResource
  : PlanningCenterSingletonFetchableResource<NamePrefix, NamePrefixResource, PeopleDocumentContext>
{

  internal NamePrefixResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People NamePrefix resources.
/// </summary>
public class NamePrefixResourceCollection
  : PlanningCenterPaginatedFetchableResource<NamePrefix, NamePrefixResourceCollection, NamePrefixResource, PeopleDocumentContext>,
  IOrderable<NamePrefixResourceCollection, NamePrefixOrderable>,
  IQueryable<NamePrefixResourceCollection, NamePrefixQueryable>
{
  internal NamePrefixResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public NamePrefixResourceCollection OrderBy(NamePrefixOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public NamePrefixResourceCollection Query(params KeyValuePair<NamePrefixQueryable, string>[] queries)
    => base.Query(queries);
}

