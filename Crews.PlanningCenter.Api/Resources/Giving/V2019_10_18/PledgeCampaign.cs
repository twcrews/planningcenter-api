/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Giving.V2019_10_18.Entities;
using Crews.PlanningCenter.Models.Giving.V2019_10_18.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Giving.V2019_10_18;

/// <summary>
/// A fetchable Giving PledgeCampaign resource.
/// </summary>
public class PledgeCampaignResource
  : PlanningCenterSingletonFetchableResource<PledgeCampaign, PledgeCampaignResource, GivingDocumentContext>,
  IIncludable<PledgeCampaignResource, PledgeCampaignIncludable>
{

  /// <summary>
  /// The related <see cref="FundResource" />.
  /// </summary>
  public FundResource Fund => GetRelated<FundResource>("fund");

  /// <summary>
  /// The related <see cref="PledgeResourceCollection" />.
  /// </summary>
  public PledgeResourceCollection Pledges => GetRelated<PledgeResourceCollection>("pledges");

  internal PledgeCampaignResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public PledgeCampaignResource Include(params PledgeCampaignIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Giving PledgeCampaign resources.
/// </summary>
public class PledgeCampaignResourceCollection
  : PlanningCenterPaginatedFetchableResource<PledgeCampaign, PledgeCampaignResourceCollection, PledgeCampaignResource, GivingDocumentContext>,
  IIncludable<PledgeCampaignResourceCollection, PledgeCampaignIncludable>,
  IOrderable<PledgeCampaignResourceCollection, PledgeCampaignOrderable>,
  IQueryable<PledgeCampaignResourceCollection, PledgeCampaignQueryable>
{
  internal PledgeCampaignResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PledgeCampaignResourceCollection Include(params PledgeCampaignIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public PledgeCampaignResourceCollection OrderBy(PledgeCampaignOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public PledgeCampaignResourceCollection Query(params KeyValuePair<PledgeCampaignQueryable, string>[] queries)
    => base.Query(queries);
}

