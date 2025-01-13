/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_11_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_11_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_11_01;

/// <summary>
/// A fetchable Services AttachmentActivity resource.
/// </summary>
public class AttachmentActivityResource
  : PlanningCenterSingletonFetchableResource<AttachmentActivity, AttachmentActivityResource, ServicesDocumentContext>
{

  internal AttachmentActivityResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services AttachmentActivity resources.
/// </summary>
public class AttachmentActivityResourceCollection
  : PlanningCenterPaginatedFetchableResource<AttachmentActivity, AttachmentActivityResourceCollection, AttachmentActivityResource, ServicesDocumentContext>
{
  internal AttachmentActivityResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

