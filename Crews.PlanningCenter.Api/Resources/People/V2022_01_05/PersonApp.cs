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
/// A fetchable People PersonApp resource.
/// </summary>
public class PersonAppResource
  : PlanningCenterSingletonFetchableResource<PersonApp, PersonAppResource, PeopleDocumentContext>,
  IIncludable<PersonAppResource, PersonAppIncludable>
{

  /// <summary>
  /// The related <see cref="AppResource" />.
  /// </summary>
  public AppResource App => GetRelated<AppResource>("app");

  internal PersonAppResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public PersonAppResource Include(params PersonAppIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People PersonApp resources.
/// </summary>
public class PersonAppResourceCollection
  : PlanningCenterPaginatedFetchableResource<PersonApp, PersonAppResourceCollection, PersonAppResource, PeopleDocumentContext>,
  IIncludable<PersonAppResourceCollection, PersonAppIncludable>
{
  internal PersonAppResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PersonAppResourceCollection Include(params PersonAppIncludable[] included)
    => base.Include(included);
}

