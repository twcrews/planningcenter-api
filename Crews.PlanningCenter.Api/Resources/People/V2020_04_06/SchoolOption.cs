/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2020_04_06.Entities;
using Crews.PlanningCenter.Models.People.V2020_04_06.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2020_04_06;

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
  public SchoolOptionResourceCollection Query(params KeyValuePair<SchoolOptionQueryable, string>[] queries)
    => base.Query(queries);
}

