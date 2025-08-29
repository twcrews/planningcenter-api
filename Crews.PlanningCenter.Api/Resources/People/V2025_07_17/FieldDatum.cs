/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2025_07_17.Entities;
using Crews.PlanningCenter.Models.People.V2025_07_17.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2025_07_17;

/// <summary>
/// A fetchable People FieldDatum resource.
/// </summary>
public class FieldDatumResource
  : PlanningCenterSingletonFetchableResource<FieldDatum, FieldDatumResource, PeopleDocumentContext>,
  IIncludable<FieldDatumResource, FieldDatumIncludable>
{

  /// <summary>
  /// The related <see cref="FieldDefinitionResource" />.
  /// </summary>
  public FieldDefinitionResource FieldDefinition => GetRelated<FieldDefinitionResource>("field_definition");

  /// <summary>
  /// The related <see cref="FieldOptionResource" />.
  /// </summary>
  public FieldOptionResource FieldOption => GetRelated<FieldOptionResource>("field_option");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  /// <summary>
  /// The related <see cref="TabResource" />.
  /// </summary>
  public TabResource Tab => GetRelated<TabResource>("tab");

  internal FieldDatumResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public FieldDatumResource Include(params FieldDatumIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<FieldDatum>> PostAsync(FieldDatum resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<FieldDatum>> PatchAsync(FieldDatum resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People FieldDatum resources.
/// </summary>
public class FieldDatumResourceCollection
  : PlanningCenterPaginatedFetchableResource<FieldDatum, FieldDatumResourceCollection, FieldDatumResource, PeopleDocumentContext>,
  IIncludable<FieldDatumResourceCollection, FieldDatumIncludable>,
  IOrderable<FieldDatumResourceCollection, FieldDatumOrderable>,
  IQueryable<FieldDatumResourceCollection, FieldDatumQueryable>
{
  internal FieldDatumResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public FieldDatumResourceCollection Include(params FieldDatumIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public FieldDatumResourceCollection OrderBy(FieldDatumOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public FieldDatumResourceCollection Query(params (FieldDatumQueryable, string)[] queries)
    => base.Query(queries);
}

