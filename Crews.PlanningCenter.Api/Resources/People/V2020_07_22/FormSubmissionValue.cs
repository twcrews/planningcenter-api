/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2020_07_22.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2020_07_22;

/// <summary>
/// A fetchable People FormSubmissionValue resource.
/// </summary>
public class FormSubmissionValueResource
  : PlanningCenterSingletonFetchableResource<FormSubmissionValue, FormSubmissionValueResource, PeopleDocumentContext>
{

  internal FormSubmissionValueResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People FormSubmissionValue resources.
/// </summary>
public class FormSubmissionValueResourceCollection
  : PlanningCenterPaginatedFetchableResource<FormSubmissionValue, FormSubmissionValueResourceCollection, FormSubmissionValueResource, PeopleDocumentContext>
{
  internal FormSubmissionValueResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

