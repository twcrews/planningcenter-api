/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2022_07_14.Entities;
using Crews.PlanningCenter.Models.People.V2022_07_14.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2022_07_14;

/// <summary>
/// A fetchable People Report resource.
/// </summary>
public class ReportResource
  : PlanningCenterSingletonFetchableResource<Report, ReportResource, PeopleDocumentContext>,
  IIncludable<ReportResource, ReportIncludable>
{

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource CreatedBy => GetRelated<PersonResource>("created_by");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource UpdatedBy => GetRelated<PersonResource>("updated_by");

  internal ReportResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public ReportResource Include(params ReportIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Report>> PostAsync(Report resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Report>> PatchAsync(Report resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People Report resources.
/// </summary>
public class ReportResourceCollection
  : PlanningCenterPaginatedFetchableResource<Report, ReportResourceCollection, ReportResource, PeopleDocumentContext>,
  IIncludable<ReportResourceCollection, ReportIncludable>,
  IOrderable<ReportResourceCollection, ReportOrderable>,
  IQueryable<ReportResourceCollection, ReportQueryable>
{
  internal ReportResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ReportResourceCollection Include(params ReportIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public ReportResourceCollection OrderBy(ReportOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public ReportResourceCollection Query(params (ReportQueryable, string)[] queries)
    => base.Query(queries);
}

