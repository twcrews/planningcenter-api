/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2025_07_02.Entities;
using Crews.PlanningCenter.Models.People.V2025_07_02.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2025_07_02;

/// <summary>
/// A fetchable People PersonMerger resource.
/// </summary>
public class PersonMergerResource
  : PlanningCenterSingletonFetchableResource<PersonMerger, PersonMergerResource, PeopleDocumentContext>
{

  internal PersonMergerResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People PersonMerger resources.
/// </summary>
public class PersonMergerResourceCollection
  : PlanningCenterPaginatedFetchableResource<PersonMerger, PersonMergerResourceCollection, PersonMergerResource, PeopleDocumentContext>,
  IOrderable<PersonMergerResourceCollection, PersonMergerOrderable>,
  IQueryable<PersonMergerResourceCollection, PersonMergerQueryable>
{
  internal PersonMergerResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PersonMergerResourceCollection OrderBy(PersonMergerOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public PersonMergerResourceCollection Query(params (PersonMergerQueryable, string)[] queries)
    => base.Query(queries);
}

