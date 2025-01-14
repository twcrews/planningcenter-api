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
/// A fetchable People PeopleImport resource.
/// </summary>
public class PeopleImportResource
  : PlanningCenterSingletonFetchableResource<PeopleImport, PeopleImportResource, PeopleDocumentContext>
{

  /// <summary>
  /// The related <see cref="PeopleImportConflictResourceCollection" />.
  /// </summary>
  public PeopleImportConflictResourceCollection Conflicts => GetRelated<PeopleImportConflictResourceCollection>("conflicts");

  /// <summary>
  /// The related <see cref="PeopleImportHistoryResourceCollection" />.
  /// </summary>
  public PeopleImportHistoryResourceCollection Histories => GetRelated<PeopleImportHistoryResourceCollection>("histories");

  internal PeopleImportResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People PeopleImport resources.
/// </summary>
public class PeopleImportResourceCollection
  : PlanningCenterPaginatedFetchableResource<PeopleImport, PeopleImportResourceCollection, PeopleImportResource, PeopleDocumentContext>,
  IQueryable<PeopleImportResourceCollection, PeopleImportQueryable>
{
  internal PeopleImportResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PeopleImportResourceCollection Query(params (PeopleImportQueryable, string)[] queries)
    => base.Query(queries);
}

