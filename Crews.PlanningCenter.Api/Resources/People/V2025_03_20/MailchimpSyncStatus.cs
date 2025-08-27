/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2025_03_20.Entities;
using Crews.PlanningCenter.Models.People.V2025_03_20.Parameters;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2025_03_20;

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

