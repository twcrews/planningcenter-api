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
/// A fetchable People BackgroundCheck resource.
/// </summary>
public class BackgroundCheckResource
  : PlanningCenterSingletonFetchableResource<BackgroundCheck, BackgroundCheckResource, PeopleDocumentContext>,
  IIncludable<BackgroundCheckResource, BackgroundCheckIncludable>
{

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal BackgroundCheckResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public BackgroundCheckResource Include(params BackgroundCheckIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People BackgroundCheck resources.
/// </summary>
public class BackgroundCheckResourceCollection
  : PlanningCenterPaginatedFetchableResource<BackgroundCheck, BackgroundCheckResourceCollection, BackgroundCheckResource, PeopleDocumentContext>,
  IIncludable<BackgroundCheckResourceCollection, BackgroundCheckIncludable>
{
  internal BackgroundCheckResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public BackgroundCheckResourceCollection Include(params BackgroundCheckIncludable[] included)
    => base.Include(included);
}

