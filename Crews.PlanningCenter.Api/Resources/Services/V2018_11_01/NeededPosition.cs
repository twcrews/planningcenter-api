/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_11_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_11_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_11_01;

/// <summary>
/// A fetchable Services NeededPosition resource.
/// </summary>
public class NeededPositionResource
  : PlanningCenterSingletonFetchableResource<NeededPosition, NeededPositionResource, ServicesDocumentContext>,
  IIncludable<NeededPositionResource, NeededPositionIncludable>
{

  /// <summary>
  /// The related <see cref="TeamResource" />.
  /// </summary>
  public TeamResource Team => GetRelated<TeamResource>("team");

  /// <summary>
  /// The related <see cref="PlanTimeResource" />.
  /// </summary>
  public PlanTimeResource Time => GetRelated<PlanTimeResource>("time");

  internal NeededPositionResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public NeededPositionResource Include(params NeededPositionIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<NeededPosition>> PostAsync(NeededPosition resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<NeededPosition>> PatchAsync(NeededPosition resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Services NeededPosition resources.
/// </summary>
public class NeededPositionResourceCollection
  : PlanningCenterPaginatedFetchableResource<NeededPosition, NeededPositionResourceCollection, NeededPositionResource, ServicesDocumentContext>,
  IIncludable<NeededPositionResourceCollection, NeededPositionIncludable>
{
  internal NeededPositionResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public NeededPositionResourceCollection Include(params NeededPositionIncludable[] included)
    => base.Include(included);
}

