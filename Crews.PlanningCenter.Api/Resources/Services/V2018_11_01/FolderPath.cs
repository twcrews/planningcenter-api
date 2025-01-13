/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_11_01.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_11_01;

/// <summary>
/// A fetchable Services FolderPath resource.
/// </summary>
public class FolderPathResource
  : PlanningCenterSingletonFetchableResource<FolderPath, FolderPathResource, ServicesDocumentContext>
{

  internal FolderPathResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services FolderPath resources.
/// </summary>
public class FolderPathResourceCollection
  : PlanningCenterPaginatedFetchableResource<FolderPath, FolderPathResourceCollection, FolderPathResource, ServicesDocumentContext>
{
  internal FolderPathResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

