/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2019_10_10.Entities;
using Crews.PlanningCenter.Models.People.V2019_10_10.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2019_10_10;

/// <summary>
/// A fetchable People CustomSender resource.
/// </summary>
public class CustomSenderResource
  : PlanningCenterSingletonFetchableResource<CustomSender, CustomSenderResource, PeopleDocumentContext>
{

  internal CustomSenderResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People CustomSender resources.
/// </summary>
public class CustomSenderResourceCollection
  : PlanningCenterPaginatedFetchableResource<CustomSender, CustomSenderResourceCollection, CustomSenderResource, PeopleDocumentContext>,
  IOrderable<CustomSenderResourceCollection, CustomSenderOrderable>
{
  internal CustomSenderResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public CustomSenderResourceCollection OrderBy(CustomSenderOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

