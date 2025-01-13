/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2019_01_14.Entities;
using Crews.PlanningCenter.Models.People.V2019_01_14.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2019_01_14;

/// <summary>
/// A fetchable People Household resource.
/// </summary>
public class HouseholdResource
  : PlanningCenterSingletonFetchableResource<Household, HouseholdResource, PeopleDocumentContext>,
  IIncludable<HouseholdResource, HouseholdIncludable>
{

  /// <summary>
  /// The related <see cref="HouseholdMembershipResourceCollection" />.
  /// </summary>
  public HouseholdMembershipResourceCollection HouseholdMemberships => GetRelated<HouseholdMembershipResourceCollection>("household_memberships");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource People => GetRelated<PersonResource>("people");

  internal HouseholdResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public HouseholdResource Include(params HouseholdIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People Household resources.
/// </summary>
public class HouseholdResourceCollection
  : PlanningCenterPaginatedFetchableResource<Household, HouseholdResourceCollection, HouseholdResource, PeopleDocumentContext>,
  IIncludable<HouseholdResourceCollection, HouseholdIncludable>,
  IOrderable<HouseholdResourceCollection, HouseholdOrderable>,
  IQueryable<HouseholdResourceCollection, HouseholdQueryable>
{
  internal HouseholdResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public HouseholdResourceCollection Include(params HouseholdIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public HouseholdResourceCollection OrderBy(HouseholdOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public HouseholdResourceCollection Query(params KeyValuePair<HouseholdQueryable, string>[] queries)
    => base.Query(queries);
}

