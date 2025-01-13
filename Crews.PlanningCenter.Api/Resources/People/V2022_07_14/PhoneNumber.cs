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
/// A fetchable People PhoneNumber resource.
/// </summary>
public class PhoneNumberResource
  : PlanningCenterSingletonFetchableResource<PhoneNumber, PhoneNumberResource, PeopleDocumentContext>
{

  internal PhoneNumberResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People PhoneNumber resources.
/// </summary>
public class PhoneNumberResourceCollection
  : PlanningCenterPaginatedFetchableResource<PhoneNumber, PhoneNumberResourceCollection, PhoneNumberResource, PeopleDocumentContext>,
  IOrderable<PhoneNumberResourceCollection, PhoneNumberOrderable>,
  IQueryable<PhoneNumberResourceCollection, PhoneNumberQueryable>
{
  internal PhoneNumberResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PhoneNumberResourceCollection OrderBy(PhoneNumberOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public PhoneNumberResourceCollection Query(params KeyValuePair<PhoneNumberQueryable, string>[] queries)
    => base.Query(queries);
}

