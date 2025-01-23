/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_11_01.Entities;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_11_01;

/// <summary>
/// A fetchable Services Zoom resource.
/// </summary>
public class ZoomResource
  : PlanningCenterSingletonFetchableResource<Zoom, ZoomResource, ServicesDocumentContext>
{

  internal ZoomResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Zoom>> PostAsync(Zoom resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Zoom>> PatchAsync(Zoom resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Services Zoom resources.
/// </summary>
public class ZoomResourceCollection
  : PlanningCenterPaginatedFetchableResource<Zoom, ZoomResourceCollection, ZoomResource, ServicesDocumentContext>
{
  internal ZoomResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

