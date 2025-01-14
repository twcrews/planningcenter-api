/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2023_02_15.Entities;
using Crews.PlanningCenter.Models.People.V2023_02_15.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2023_02_15;

/// <summary>
/// A fetchable People Tab resource.
/// </summary>
public class TabResource
  : PlanningCenterSingletonFetchableResource<Tab, TabResource, PeopleDocumentContext>,
  IIncludable<TabResource, TabIncludable>
{

  /// <summary>
  /// The related <see cref="FieldDefinitionResourceCollection" />.
  /// </summary>
  public FieldDefinitionResourceCollection FieldDefinitions => GetRelated<FieldDefinitionResourceCollection>("field_definitions");

  /// <summary>
  /// The related <see cref="FieldOptionResourceCollection" />.
  /// </summary>
  public FieldOptionResourceCollection FieldOptions => GetRelated<FieldOptionResourceCollection>("field_options");

  internal TabResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public TabResource Include(params TabIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Tab>> PostAsync(Tab resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Tab>> PatchAsync(Tab resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People Tab resources.
/// </summary>
public class TabResourceCollection
  : PlanningCenterPaginatedFetchableResource<Tab, TabResourceCollection, TabResource, PeopleDocumentContext>,
  IIncludable<TabResourceCollection, TabIncludable>,
  IOrderable<TabResourceCollection, TabOrderable>,
  IQueryable<TabResourceCollection, TabQueryable>
{
  internal TabResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public TabResourceCollection Include(params TabIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public TabResourceCollection OrderBy(TabOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public TabResourceCollection Query(params (TabQueryable, string)[] queries)
    => base.Query(queries);
}

