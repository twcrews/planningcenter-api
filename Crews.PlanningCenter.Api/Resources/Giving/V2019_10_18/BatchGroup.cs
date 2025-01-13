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
/// A fetchable Giving BatchGroup resource.
/// </summary>
public class BatchGroupResource
  : PlanningCenterSingletonFetchableResource<BatchGroup, BatchGroupResource, GivingDocumentContext>,
  IIncludable<BatchGroupResource, BatchGroupIncludable>
{

  /// <summary>
  /// The related <see cref="BatchResourceCollection" />.
  /// </summary>
  public BatchResourceCollection Batches => GetRelated<BatchResourceCollection>("batches");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Owner => GetRelated<PersonResource>("owner");

  internal BatchGroupResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public BatchGroupResource Include(params BatchGroupIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Giving BatchGroup resources.
/// </summary>
public class BatchGroupResourceCollection
  : PlanningCenterPaginatedFetchableResource<BatchGroup, BatchGroupResourceCollection, BatchGroupResource, GivingDocumentContext>,
  IIncludable<BatchGroupResourceCollection, BatchGroupIncludable>,
  IOrderable<BatchGroupResourceCollection, BatchGroupOrderable>,
  IQueryable<BatchGroupResourceCollection, BatchGroupQueryable>
{
  internal BatchGroupResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public BatchGroupResourceCollection Include(params BatchGroupIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public BatchGroupResourceCollection OrderBy(BatchGroupOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public BatchGroupResourceCollection Query(params KeyValuePair<BatchGroupQueryable, string>[] queries)
    => base.Query(queries);
}

