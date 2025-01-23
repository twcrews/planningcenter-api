/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2022_01_05.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2022_01_05;

/// <summary>
/// A fetchable People BirthdayPeople resource.
/// </summary>
public class BirthdayPeopleResource
  : PlanningCenterSingletonFetchableResource<BirthdayPeople, BirthdayPeopleResource, PeopleDocumentContext>
{

  internal BirthdayPeopleResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People BirthdayPeople resources.
/// </summary>
public class BirthdayPeopleResourceCollection
  : PlanningCenterPaginatedFetchableResource<BirthdayPeople, BirthdayPeopleResourceCollection, BirthdayPeopleResource, PeopleDocumentContext>
{
  internal BirthdayPeopleResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

