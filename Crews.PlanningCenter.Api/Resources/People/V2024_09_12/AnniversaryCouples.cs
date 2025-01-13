/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2024_09_12.Entities;
using Crews.PlanningCenter.Models.People.V2024_09_12.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2024_09_12;

/// <summary>
/// A fetchable People AnniversaryCouples resource.
/// </summary>
public class AnniversaryCouplesResource
  : PlanningCenterSingletonFetchableResource<AnniversaryCouples, AnniversaryCouplesResource, PeopleDocumentContext>
{

  internal AnniversaryCouplesResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People AnniversaryCouples resources.
/// </summary>
public class AnniversaryCouplesResourceCollection
  : PlanningCenterPaginatedFetchableResource<AnniversaryCouples, AnniversaryCouplesResourceCollection, AnniversaryCouplesResource, PeopleDocumentContext>
{
  internal AnniversaryCouplesResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

