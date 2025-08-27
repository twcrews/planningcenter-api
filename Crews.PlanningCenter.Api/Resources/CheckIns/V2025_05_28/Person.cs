/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2025_05_28.Entities;
using Crews.PlanningCenter.Models.CheckIns.V2025_05_28.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2025_05_28;

/// <summary>
/// A fetchable CheckIns Person resource.
/// </summary>
public class PersonResource
  : PlanningCenterSingletonFetchableResource<Person, PersonResource, CheckInsDocumentContext>,
  IIncludable<PersonResource, PersonIncludable>
{

  /// <summary>
  /// The related <see cref="CheckInResourceCollection" />.
  /// </summary>
  public CheckInResourceCollection CheckIns => GetRelated<CheckInResourceCollection>("check_ins");

  /// <summary>
  /// The related <see cref="OrganizationResource" />.
  /// </summary>
  public OrganizationResource Organization => GetRelated<OrganizationResource>("organization");

  /// <summary>
  /// The related <see cref="PassResourceCollection" />.
  /// </summary>
  public PassResourceCollection Passes => GetRelated<PassResourceCollection>("passes");

  /// <summary>
  /// The related <see cref="PersonEventResourceCollection" />.
  /// </summary>
  public PersonEventResourceCollection PersonEvents => GetRelated<PersonEventResourceCollection>("person_events");

  internal PersonResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public PersonResource Include(params PersonIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of CheckIns Person resources.
/// </summary>
public class PersonResourceCollection
  : PlanningCenterPaginatedFetchableResource<Person, PersonResourceCollection, PersonResource, CheckInsDocumentContext>,
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

