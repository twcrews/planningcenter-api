/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2025_07_02.Entities;
using Crews.PlanningCenter.Models.People.V2025_07_02.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2025_07_02;

/// <summary>
/// A fetchable People Rule resource.
/// </summary>
public class RuleResource
  : PlanningCenterSingletonFetchableResource<Rule, RuleResource, PeopleDocumentContext>,
  IIncludable<RuleResource, RuleIncludable>
{

  /// <summary>
  /// The related <see cref="ConditionResourceCollection" />.
  /// </summary>
  public ConditionResourceCollection Conditions => GetRelated<ConditionResourceCollection>("conditions");

  internal RuleResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public RuleResource Include(params RuleIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People Rule resources.
/// </summary>
public class RuleResourceCollection
  : PlanningCenterPaginatedFetchableResource<Rule, RuleResourceCollection, RuleResource, PeopleDocumentContext>,
  IIncludable<RuleResourceCollection, RuleIncludable>,
  IOrderable<RuleResourceCollection, RuleOrderable>,
  IQueryable<RuleResourceCollection, RuleQueryable>
{
  internal RuleResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public RuleResourceCollection Include(params RuleIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public RuleResourceCollection OrderBy(RuleOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public RuleResourceCollection Query(params (RuleQueryable, string)[] queries)
    => base.Query(queries);
}

