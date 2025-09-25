/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2020_04_06.Entities;
using Crews.PlanningCenter.Models.People.V2020_04_06.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2020_04_06;

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
  /// The related <see cref="PersonResourceCollection" />.
  /// </summary>
  public PersonResourceCollection People => GetRelated<PersonResourceCollection>("people");

  internal HouseholdResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public HouseholdResource Include(params HouseholdIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Household>> PostAsync(Household resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Household>> PatchAsync(Household resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
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
  public HouseholdResourceCollection Query(params (HouseholdQueryable, string)[] queries)
    => base.Query(queries);
}

