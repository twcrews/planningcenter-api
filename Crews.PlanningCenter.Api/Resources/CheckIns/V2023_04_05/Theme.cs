/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2023_04_05.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2023_04_05;

/// <summary>
/// A fetchable CheckIns Theme resource.
/// </summary>
public class ThemeResource
  : PlanningCenterSingletonFetchableResource<Theme, ThemeResource, CheckInsDocumentContext>
{

  internal ThemeResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of CheckIns Theme resources.
/// </summary>
public class ThemeResourceCollection
  : PlanningCenterPaginatedFetchableResource<Theme, ThemeResourceCollection, ThemeResource, CheckInsDocumentContext>
{
  internal ThemeResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

