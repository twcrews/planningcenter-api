/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2023_03_21.Entities;
using Crews.PlanningCenter.Models.People.V2023_03_21.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2023_03_21;

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

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<BackgroundCheck>> PostAsync(BackgroundCheck resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<BackgroundCheck>> PatchAsync(BackgroundCheck resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
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

