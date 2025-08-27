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
/// A fetchable Services ItemNoteCategory resource.
/// </summary>
public class ItemNoteCategoryResource
  : PlanningCenterSingletonFetchableResource<ItemNoteCategory, ItemNoteCategoryResource, ServicesDocumentContext>
{

  internal ItemNoteCategoryResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services ItemNoteCategory resources.
/// </summary>
public class ItemNoteCategoryResourceCollection
  : PlanningCenterPaginatedFetchableResource<ItemNoteCategory, ItemNoteCategoryResourceCollection, ItemNoteCategoryResource, ServicesDocumentContext>
{
  internal ItemNoteCategoryResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

