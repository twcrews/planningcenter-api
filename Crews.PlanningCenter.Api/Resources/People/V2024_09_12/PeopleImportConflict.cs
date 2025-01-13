/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2024_09_12.Entities;
using Crews.PlanningCenter.Models.People.V2024_09_12.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2024_09_12;

/// <summary>
/// A fetchable People PeopleImportConflict resource.
/// </summary>
public class PeopleImportConflictResource
  : PlanningCenterSingletonFetchableResource<PeopleImportConflict, PeopleImportConflictResource, PeopleDocumentContext>
{

  internal PeopleImportConflictResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People PeopleImportConflict resources.
/// </summary>
public class PeopleImportConflictResourceCollection
  : PlanningCenterPaginatedFetchableResource<PeopleImportConflict, PeopleImportConflictResourceCollection, PeopleImportConflictResource, PeopleDocumentContext>,
  IQueryable<PeopleImportConflictResourceCollection, PeopleImportConflictQueryable>
{
  internal PeopleImportConflictResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PeopleImportConflictResourceCollection Query(params KeyValuePair<PeopleImportConflictQueryable, string>[] queries)
    => base.Query(queries);
}

