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
/// A fetchable Services Chat resource.
/// </summary>
public class ChatResource
  : PlanningCenterSingletonFetchableResource<Chat, ChatResource, ServicesDocumentContext>
{

  internal ChatResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services Chat resources.
/// </summary>
public class ChatResourceCollection
  : PlanningCenterPaginatedFetchableResource<Chat, ChatResourceCollection, ChatResource, ServicesDocumentContext>
{
  internal ChatResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

