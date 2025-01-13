/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2022_01_05.Entities;
using Crews.PlanningCenter.Models.People.V2022_01_05.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2022_01_05;

/// <summary>
/// A fetchable People Condition resource.
/// </summary>
public class ConditionResource
  : PlanningCenterSingletonFetchableResource<Condition, ConditionResource, PeopleDocumentContext>,
  IIncludable<ConditionResource, ConditionIncludable>
{

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource CreatedBy => GetRelated<PersonResource>("created_by");

  internal ConditionResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public ConditionResource Include(params ConditionIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People Condition resources.
/// </summary>
public class ConditionResourceCollection
  : PlanningCenterPaginatedFetchableResource<Condition, ConditionResourceCollection, ConditionResource, PeopleDocumentContext>,
  IIncludable<ConditionResourceCollection, ConditionIncludable>,
  IOrderable<ConditionResourceCollection, ConditionOrderable>,
  IQueryable<ConditionResourceCollection, ConditionQueryable>
{
  internal ConditionResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ConditionResourceCollection Include(params ConditionIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public ConditionResourceCollection OrderBy(ConditionOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public ConditionResourceCollection Query(params KeyValuePair<ConditionQueryable, string>[] queries)
    => base.Query(queries);
}

