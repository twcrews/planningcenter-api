/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2025_03_20.Entities;
using Crews.PlanningCenter.Models.People.V2025_03_20.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2025_03_20;

/// <summary>
/// A fetchable People SchoolOption resource.
/// </summary>
public class SchoolOptionResource
  : PlanningCenterSingletonFetchableResource<SchoolOption, SchoolOptionResource, PeopleDocumentContext>
{

  /// <summary>
  /// The related <see cref="SchoolOptionResource" />.
  /// </summary>
  public SchoolOptionResource PromotesToSchool => GetRelated<SchoolOptionResource>("promotes_to_school");

  internal SchoolOptionResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<SchoolOption>> PostAsync(SchoolOption resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<SchoolOption>> PatchAsync(SchoolOption resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People SchoolOption resources.
/// </summary>
public class SchoolOptionResourceCollection
  : PlanningCenterPaginatedFetchableResource<SchoolOption, SchoolOptionResourceCollection, SchoolOptionResource, PeopleDocumentContext>,
  IOrderable<SchoolOptionResourceCollection, SchoolOptionOrderable>,
  IQueryable<SchoolOptionResourceCollection, SchoolOptionQueryable>
{
  internal SchoolOptionResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public SchoolOptionResourceCollection OrderBy(SchoolOptionOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public SchoolOptionResourceCollection Query(params (SchoolOptionQueryable, string)[] queries)
    => base.Query(queries);
}

