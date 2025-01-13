/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2021_08_17.Entities;
using Crews.PlanningCenter.Models.People.V2021_08_17.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2021_08_17;

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

