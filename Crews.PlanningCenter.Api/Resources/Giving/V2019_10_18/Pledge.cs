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
/// A fetchable Giving Pledge resource.
/// </summary>
public class PledgeResource
  : PlanningCenterSingletonFetchableResource<Pledge, PledgeResource, GivingDocumentContext>,
  IIncludable<PledgeResource, PledgeIncludable>
{

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource JointGiver => GetRelated<PersonResource>("joint_giver");

  /// <summary>
  /// The related <see cref="PledgeCampaignResource" />.
  /// </summary>
  public PledgeCampaignResource PledgeCampaign => GetRelated<PledgeCampaignResource>("pledge_campaign");

  internal PledgeResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public PledgeResource Include(params PledgeIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Pledge>> PostAsync(Pledge resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Pledge>> PatchAsync(Pledge resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Giving Pledge resources.
/// </summary>
public class PledgeResourceCollection
  : PlanningCenterPaginatedFetchableResource<Pledge, PledgeResourceCollection, PledgeResource, GivingDocumentContext>,
  IIncludable<PledgeResourceCollection, PledgeIncludable>,
  IOrderable<PledgeResourceCollection, PledgeOrderable>,
  IQueryable<PledgeResourceCollection, PledgeQueryable>
{
  internal PledgeResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PledgeResourceCollection Include(params PledgeIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public PledgeResourceCollection OrderBy(PledgeOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public PledgeResourceCollection Query(params (PledgeQueryable, string)[] queries)
    => base.Query(queries);
}

