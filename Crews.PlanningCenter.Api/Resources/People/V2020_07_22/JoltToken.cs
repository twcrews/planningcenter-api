/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2020_07_22.Entities;
using Crews.PlanningCenter.Models.People.V2020_07_22.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2020_07_22;

/// <summary>
/// A fetchable People JoltToken resource.
/// </summary>
public class JoltTokenResource
  : PlanningCenterSingletonFetchableResource<JoltToken, JoltTokenResource, PeopleDocumentContext>
{

  internal JoltTokenResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People JoltToken resources.
/// </summary>
public class JoltTokenResourceCollection
  : PlanningCenterPaginatedFetchableResource<JoltToken, JoltTokenResourceCollection, JoltTokenResource, PeopleDocumentContext>
{
  internal JoltTokenResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

