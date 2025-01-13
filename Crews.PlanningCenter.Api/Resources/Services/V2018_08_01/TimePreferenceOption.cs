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
/// A fetchable Services TimePreferenceOption resource.
/// </summary>
public class TimePreferenceOptionResource
  : PlanningCenterSingletonFetchableResource<TimePreferenceOption, TimePreferenceOptionResource, ServicesDocumentContext>
{

  internal TimePreferenceOptionResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services TimePreferenceOption resources.
/// </summary>
public class TimePreferenceOptionResourceCollection
  : PlanningCenterPaginatedFetchableResource<TimePreferenceOption, TimePreferenceOptionResourceCollection, TimePreferenceOptionResource, ServicesDocumentContext>,
  IOrderable<TimePreferenceOptionResourceCollection, TimePreferenceOptionOrderable>
{
  internal TimePreferenceOptionResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public TimePreferenceOptionResourceCollection OrderBy(TimePreferenceOptionOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

