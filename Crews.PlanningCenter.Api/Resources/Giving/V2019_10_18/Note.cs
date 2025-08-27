/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Giving.V2019_10_18.Entities;
using Crews.PlanningCenter.Models.Giving.V2019_10_18.Parameters;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Giving.V2019_10_18;

/// <summary>
/// A fetchable Giving Note resource.
/// </summary>
public class NoteResource
  : PlanningCenterSingletonFetchableResource<Note, NoteResource, GivingDocumentContext>
{

  internal NoteResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Giving Note resources.
/// </summary>
public class NoteResourceCollection
  : PlanningCenterPaginatedFetchableResource<Note, NoteResourceCollection, NoteResource, GivingDocumentContext>
{
  internal NoteResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

