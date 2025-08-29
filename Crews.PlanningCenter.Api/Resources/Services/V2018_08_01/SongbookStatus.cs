/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services SongbookStatus resource.
/// </summary>
public class SongbookStatusResource
  : PlanningCenterSingletonFetchableResource<SongbookStatus, SongbookStatusResource, ServicesDocumentContext>
{

  internal SongbookStatusResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services SongbookStatus resources.
/// </summary>
public class SongbookStatusResourceCollection
  : PlanningCenterPaginatedFetchableResource<SongbookStatus, SongbookStatusResourceCollection, SongbookStatusResource, ServicesDocumentContext>
{
  internal SongbookStatusResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

