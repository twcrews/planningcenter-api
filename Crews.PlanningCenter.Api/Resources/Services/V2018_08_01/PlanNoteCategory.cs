/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services PlanNoteCategory resource.
/// </summary>
public class PlanNoteCategoryResource
  : PlanningCenterSingletonFetchableResource<PlanNoteCategory, PlanNoteCategoryResource, ServicesDocumentContext>
{

  internal PlanNoteCategoryResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services PlanNoteCategory resources.
/// </summary>
public class PlanNoteCategoryResourceCollection
  : PlanningCenterPaginatedFetchableResource<PlanNoteCategory, PlanNoteCategoryResourceCollection, PlanNoteCategoryResource, ServicesDocumentContext>
{
  internal PlanNoteCategoryResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

