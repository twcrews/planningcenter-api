/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Publishing.V2018_08_01.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Publishing.V2018_08_01;

/// <summary>
/// A fetchable Publishing NoteTemplate resource.
/// </summary>
public class NoteTemplateResource
  : PlanningCenterSingletonFetchableResource<NoteTemplate, NoteTemplateResource, PublishingDocumentContext>
{

  internal NoteTemplateResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Publishing NoteTemplate resources.
/// </summary>
public class NoteTemplateResourceCollection
  : PlanningCenterPaginatedFetchableResource<NoteTemplate, NoteTemplateResourceCollection, NoteTemplateResource, PublishingDocumentContext>
{
  internal NoteTemplateResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

