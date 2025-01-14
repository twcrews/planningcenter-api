/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2021_07_20.Entities;
using Crews.PlanningCenter.Models.Calendar.V2021_07_20.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2021_07_20;

/// <summary>
/// A fetchable Calendar Person resource.
/// </summary>
public class PersonResource
  : PlanningCenterSingletonFetchableResource<Person, PersonResource, CalendarDocumentContext>,
  IIncludable<PersonResource, PersonIncludable>
{

  /// <summary>
  /// The related <see cref="EventResourceRequestResourceCollection" />.
  /// </summary>
  public EventResourceRequestResourceCollection EventResourceRequests => GetRelated<EventResourceRequestResourceCollection>("event_resource_requests");

  /// <summary>
  /// The related <see cref="OrganizationResource" />.
  /// </summary>
  public OrganizationResource Organization => GetRelated<OrganizationResource>("organization");

  internal PersonResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public PersonResource Include(params PersonIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Calendar Person resources.
/// </summary>
public class PersonResourceCollection
  : PlanningCenterPaginatedFetchableResource<Person, PersonResourceCollection, PersonResource, CalendarDocumentContext>,
  IIncludable<PersonResourceCollection, PersonIncludable>,
  IOrderable<PersonResourceCollection, PersonOrderable>,
  IQueryable<PersonResourceCollection, PersonQueryable>
{
  internal PersonResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PersonResourceCollection Include(params PersonIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public PersonResourceCollection OrderBy(PersonOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public PersonResourceCollection Query(params (PersonQueryable, string)[] queries)
    => base.Query(queries);
}

