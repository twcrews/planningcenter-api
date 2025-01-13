/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Giving.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Giving.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Giving.V2018_08_01;

/// <summary>
/// A fetchable Giving Batch resource.
/// </summary>
public class BatchResource
  : PlanningCenterSingletonFetchableResource<Batch, BatchResource, GivingDocumentContext>,
  IIncludable<BatchResource, BatchIncludable>
{

  /// <summary>
  /// The related <see cref="BatchGroupResource" />.
  /// </summary>
  public BatchGroupResource BatchGroup => GetRelated<BatchGroupResource>("batch_group");

  /// <summary>
  /// The related <see cref="DonationResourceCollection" />.
  /// </summary>
  public DonationResourceCollection Donations => GetRelated<DonationResourceCollection>("donations");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Owner => GetRelated<PersonResource>("owner");

  internal BatchResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public BatchResource Include(params BatchIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Giving Batch resources.
/// </summary>
public class BatchResourceCollection
  : PlanningCenterPaginatedFetchableResource<Batch, BatchResourceCollection, BatchResource, GivingDocumentContext>,
  IIncludable<BatchResourceCollection, BatchIncludable>,
  IOrderable<BatchResourceCollection, BatchOrderable>,
  IQueryable<BatchResourceCollection, BatchQueryable>
{
  internal BatchResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public BatchResourceCollection Include(params BatchIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public BatchResourceCollection OrderBy(BatchOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public BatchResourceCollection Query(params KeyValuePair<BatchQueryable, string>[] queries)
    => base.Query(queries);
}

