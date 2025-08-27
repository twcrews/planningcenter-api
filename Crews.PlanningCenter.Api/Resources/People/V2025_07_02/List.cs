/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2025_07_02.Entities;
using Crews.PlanningCenter.Models.People.V2025_07_02.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2025_07_02;

/// <summary>
/// A fetchable People List resource.
/// </summary>
public class ListResource
  : PlanningCenterSingletonFetchableResource<List, ListResource, PeopleDocumentContext>,
  IIncludable<ListResource, ListIncludable>
{

  /// <summary>
  /// The related <see cref="CampusResource" />.
  /// </summary>
  public CampusResource Campus => GetRelated<CampusResource>("campus");

  /// <summary>
  /// The related <see cref="ListCategoryResource" />.
  /// </summary>
  public ListCategoryResource Category => GetRelated<ListCategoryResource>("category");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource CreatedBy => GetRelated<PersonResource>("created_by");

  /// <summary>
  /// The related <see cref="ListResultResourceCollection" />.
  /// </summary>
  public ListResultResourceCollection ListResults => GetRelated<ListResultResourceCollection>("list_results");

  /// <summary>
  /// The related <see cref="MailchimpSyncStatusResource" />.
  /// </summary>
  public MailchimpSyncStatusResource MailchimpSyncStatus => GetRelated<MailchimpSyncStatusResource>("mailchimp_sync_status");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource People => GetRelated<PersonResource>("people");

  /// <summary>
  /// The related <see cref="RuleResourceCollection" />.
  /// </summary>
  public RuleResourceCollection Rules => GetRelated<RuleResourceCollection>("rules");

  /// <summary>
  /// The related <see cref="ListShareResourceCollection" />.
  /// </summary>
  public ListShareResourceCollection Shares => GetRelated<ListShareResourceCollection>("shares");

  /// <summary>
  /// The related <see cref="ListStarResource" />.
  /// </summary>
  public ListStarResource Star => GetRelated<ListStarResource>("star");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource UpdatedBy => GetRelated<PersonResource>("updated_by");

  internal ListResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public ListResource Include(params ListIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<List>> PatchAsync(List resource)
    => base.PatchAsync(resource);
}

/// <summary>
/// A fetchable collection of People List resources.
/// </summary>
public class ListResourceCollection
  : PlanningCenterPaginatedFetchableResource<List, ListResourceCollection, ListResource, PeopleDocumentContext>,
  IIncludable<ListResourceCollection, ListIncludable>,
  IOrderable<ListResourceCollection, ListOrderable>,
  IQueryable<ListResourceCollection, ListQueryable>
{
  internal ListResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ListResourceCollection Include(params ListIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public ListResourceCollection OrderBy(ListOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public ListResourceCollection Query(params (ListQueryable, string)[] queries)
    => base.Query(queries);
}

