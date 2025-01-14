/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2022_01_28.Entities;
using Crews.PlanningCenter.Models.People.V2022_01_28.Parameters;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2022_01_28;

/// <summary>
/// A fetchable People PlatformNotification resource.
/// </summary>
public class PlatformNotificationResource
  : PlanningCenterSingletonFetchableResource<PlatformNotification, PlatformNotificationResource, PeopleDocumentContext>
{

  internal PlatformNotificationResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People PlatformNotification resources.
/// </summary>
public class PlatformNotificationResourceCollection
  : PlanningCenterPaginatedFetchableResource<PlatformNotification, PlatformNotificationResourceCollection, PlatformNotificationResource, PeopleDocumentContext>
{
  internal PlatformNotificationResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

