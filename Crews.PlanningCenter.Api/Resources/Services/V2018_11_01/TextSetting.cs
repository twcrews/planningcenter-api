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
/// A fetchable Services TextSetting resource.
/// </summary>
public class TextSettingResource
  : PlanningCenterSingletonFetchableResource<TextSetting, TextSettingResource, ServicesDocumentContext>
{

  internal TextSettingResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<TextSetting>> PatchAsync(TextSetting resource)
    => base.PatchAsync(resource);
}

/// <summary>
/// A fetchable collection of Services TextSetting resources.
/// </summary>
public class TextSettingResourceCollection
  : PlanningCenterPaginatedFetchableResource<TextSetting, TextSettingResourceCollection, TextSettingResource, ServicesDocumentContext>
{
  internal TextSettingResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

