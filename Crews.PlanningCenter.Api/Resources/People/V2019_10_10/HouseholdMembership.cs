/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2019_10_10.Entities;
using Crews.PlanningCenter.Models.People.V2019_10_10.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2019_10_10;

/// <summary>
/// A fetchable People HouseholdMembership resource.
/// </summary>
public class HouseholdMembershipResource
  : PlanningCenterSingletonFetchableResource<HouseholdMembership, HouseholdMembershipResource, PeopleDocumentContext>,
  IIncludable<HouseholdMembershipResource, HouseholdMembershipIncludable>
{

  /// <summary>
  /// The related <see cref="HouseholdResource" />.
  /// </summary>
  public HouseholdResource Household => GetRelated<HouseholdResource>("household");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal HouseholdMembershipResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public HouseholdMembershipResource Include(params HouseholdMembershipIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<HouseholdMembership>> PostAsync(HouseholdMembership resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<HouseholdMembership>> PatchAsync(HouseholdMembership resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People HouseholdMembership resources.
/// </summary>
public class HouseholdMembershipResourceCollection
  : PlanningCenterPaginatedFetchableResource<HouseholdMembership, HouseholdMembershipResourceCollection, HouseholdMembershipResource, PeopleDocumentContext>,
  IIncludable<HouseholdMembershipResourceCollection, HouseholdMembershipIncludable>,
  IOrderable<HouseholdMembershipResourceCollection, HouseholdMembershipOrderable>,
  IQueryable<HouseholdMembershipResourceCollection, HouseholdMembershipQueryable>
{
  internal HouseholdMembershipResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public HouseholdMembershipResourceCollection Include(params HouseholdMembershipIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public HouseholdMembershipResourceCollection OrderBy(HouseholdMembershipOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public HouseholdMembershipResourceCollection Query(params (HouseholdMembershipQueryable, string)[] queries)
    => base.Query(queries);
}

