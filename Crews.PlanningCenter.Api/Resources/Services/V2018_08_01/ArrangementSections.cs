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
/// A fetchable Services ArrangementSections resource.
/// </summary>
public class ArrangementSectionsResource
  : PlanningCenterSingletonFetchableResource<ArrangementSections, ArrangementSectionsResource, ServicesDocumentContext>
{

  internal ArrangementSectionsResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services ArrangementSections resources.
/// </summary>
public class ArrangementSectionsResourceCollection
  : PlanningCenterPaginatedFetchableResource<ArrangementSections, ArrangementSectionsResourceCollection, ArrangementSectionsResource, ServicesDocumentContext>
{
  internal ArrangementSectionsResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

