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
/// A fetchable People PeopleImportHistory resource.
/// </summary>
public class PeopleImportHistoryResource
  : PlanningCenterSingletonFetchableResource<PeopleImportHistory, PeopleImportHistoryResource, PeopleDocumentContext>,
  IIncludable<PeopleImportHistoryResource, PeopleImportHistoryIncludable>
{

  /// <summary>
  /// The related <see cref="HouseholdResource" />.
  /// </summary>
  public HouseholdResource Household => GetRelated<HouseholdResource>("household");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal PeopleImportHistoryResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public PeopleImportHistoryResource Include(params PeopleImportHistoryIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People PeopleImportHistory resources.
/// </summary>
public class PeopleImportHistoryResourceCollection
  : PlanningCenterPaginatedFetchableResource<PeopleImportHistory, PeopleImportHistoryResourceCollection, PeopleImportHistoryResource, PeopleDocumentContext>,
  IIncludable<PeopleImportHistoryResourceCollection, PeopleImportHistoryIncludable>,
  IQueryable<PeopleImportHistoryResourceCollection, PeopleImportHistoryQueryable>
{
  internal PeopleImportHistoryResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PeopleImportHistoryResourceCollection Include(params PeopleImportHistoryIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public PeopleImportHistoryResourceCollection Query(params (PeopleImportHistoryQueryable, string)[] queries)
    => base.Query(queries);
}

