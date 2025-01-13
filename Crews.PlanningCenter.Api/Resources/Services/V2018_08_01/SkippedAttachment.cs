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
/// A fetchable Services SkippedAttachment resource.
/// </summary>
public class SkippedAttachmentResource
  : PlanningCenterSingletonFetchableResource<SkippedAttachment, SkippedAttachmentResource, ServicesDocumentContext>
{

  internal SkippedAttachmentResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services SkippedAttachment resources.
/// </summary>
public class SkippedAttachmentResourceCollection
  : PlanningCenterPaginatedFetchableResource<SkippedAttachment, SkippedAttachmentResourceCollection, SkippedAttachmentResource, ServicesDocumentContext>
{
  internal SkippedAttachmentResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

