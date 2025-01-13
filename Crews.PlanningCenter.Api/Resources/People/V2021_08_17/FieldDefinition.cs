/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2021_08_17.Entities;
using Crews.PlanningCenter.Models.People.V2021_08_17.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2021_08_17;

/// <summary>
/// A fetchable People FieldDefinition resource.
/// </summary>
public class FieldDefinitionResource
  : PlanningCenterSingletonFetchableResource<FieldDefinition, FieldDefinitionResource, PeopleDocumentContext>,
  IIncludable<FieldDefinitionResource, FieldDefinitionIncludable>
{

  /// <summary>
  /// The related <see cref="FieldOptionResourceCollection" />.
  /// </summary>
  public FieldOptionResourceCollection FieldOptions => GetRelated<FieldOptionResourceCollection>("field_options");

  /// <summary>
  /// The related <see cref="TabResource" />.
  /// </summary>
  public TabResource Tab => GetRelated<TabResource>("tab");

  internal FieldDefinitionResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public FieldDefinitionResource Include(params FieldDefinitionIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People FieldDefinition resources.
/// </summary>
public class FieldDefinitionResourceCollection
  : PlanningCenterPaginatedFetchableResource<FieldDefinition, FieldDefinitionResourceCollection, FieldDefinitionResource, PeopleDocumentContext>,
  IIncludable<FieldDefinitionResourceCollection, FieldDefinitionIncludable>,
  IOrderable<FieldDefinitionResourceCollection, FieldDefinitionOrderable>,
  IQueryable<FieldDefinitionResourceCollection, FieldDefinitionQueryable>
{
  internal FieldDefinitionResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public FieldDefinitionResourceCollection Include(params FieldDefinitionIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public FieldDefinitionResourceCollection OrderBy(FieldDefinitionOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public FieldDefinitionResourceCollection Query(params KeyValuePair<FieldDefinitionQueryable, string>[] queries)
    => base.Query(queries);
}

