/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2019_01_14.Entities;
using Crews.PlanningCenter.Models.People.V2019_01_14.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2019_01_14;

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

