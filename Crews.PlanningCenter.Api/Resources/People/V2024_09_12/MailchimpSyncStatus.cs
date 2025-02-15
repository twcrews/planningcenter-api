/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2024_09_12.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2024_09_12;

/// <summary>
/// A fetchable People MailchimpSyncStatus resource.
/// </summary>
public class MailchimpSyncStatusResource
  : PlanningCenterSingletonFetchableResource<MailchimpSyncStatus, MailchimpSyncStatusResource, PeopleDocumentContext>
{

  internal MailchimpSyncStatusResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People MailchimpSyncStatus resources.
/// </summary>
public class MailchimpSyncStatusResourceCollection
  : PlanningCenterPaginatedFetchableResource<MailchimpSyncStatus, MailchimpSyncStatusResourceCollection, MailchimpSyncStatusResource, PeopleDocumentContext>
{
  internal MailchimpSyncStatusResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

