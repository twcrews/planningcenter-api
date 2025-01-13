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
/// A fetchable People FormFieldOption resource.
/// </summary>
public class FormFieldOptionResource
  : PlanningCenterSingletonFetchableResource<FormFieldOption, FormFieldOptionResource, PeopleDocumentContext>
{

  internal FormFieldOptionResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People FormFieldOption resources.
/// </summary>
public class FormFieldOptionResourceCollection
  : PlanningCenterPaginatedFetchableResource<FormFieldOption, FormFieldOptionResourceCollection, FormFieldOptionResource, PeopleDocumentContext>,
  IOrderable<FormFieldOptionResourceCollection, FormFieldOptionOrderable>
{
  internal FormFieldOptionResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public FormFieldOptionResourceCollection OrderBy(FormFieldOptionOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

