/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Giving.V2019_10_18.Entities;
using Crews.PlanningCenter.Models.Giving.V2019_10_18.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.Giving.V2019_10_18;

/// <summary>
/// A fetchable Giving InKindDonation resource.
/// </summary>
public class InKindDonationResource
  : PlanningCenterSingletonFetchableResource<InKindDonation, InKindDonationResource, GivingDocumentContext>,
  IIncludable<InKindDonationResource, InKindDonationIncludable>
{

  /// <summary>
  /// The related <see cref="CampusResource" />.
  /// </summary>
  public CampusResource Campus => GetRelated<CampusResource>("campus");

  /// <summary>
  /// The related <see cref="FundResource" />.
  /// </summary>
  public FundResource Fund => GetRelated<FundResource>("fund");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal InKindDonationResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public InKindDonationResource Include(params InKindDonationIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<InKindDonation>> PostAsync(InKindDonation resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<InKindDonation>> PatchAsync(InKindDonation resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Giving InKindDonation resources.
/// </summary>
public class InKindDonationResourceCollection
  : PlanningCenterPaginatedFetchableResource<InKindDonation, InKindDonationResourceCollection, InKindDonationResource, GivingDocumentContext>,
  IIncludable<InKindDonationResourceCollection, InKindDonationIncludable>,
  IOrderable<InKindDonationResourceCollection, InKindDonationOrderable>,
  IQueryable<InKindDonationResourceCollection, InKindDonationQueryable>
{
  internal InKindDonationResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public InKindDonationResourceCollection Include(params InKindDonationIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public InKindDonationResourceCollection OrderBy(InKindDonationOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public InKindDonationResourceCollection Query(params (InKindDonationQueryable, string)[] queries)
    => base.Query(queries);
}

