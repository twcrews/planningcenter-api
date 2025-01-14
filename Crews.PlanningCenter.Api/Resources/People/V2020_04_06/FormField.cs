/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2020_04_06.Entities;
using Crews.PlanningCenter.Models.People.V2020_04_06.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2020_04_06;

/// <summary>
/// A fetchable People FormField resource.
/// </summary>
public class FormFieldResource
  : PlanningCenterSingletonFetchableResource<FormField, FormFieldResource, PeopleDocumentContext>,
  IIncludable<FormFieldResource, FormFieldIncludable>
{

  /// <summary>
  /// The related <see cref="FormFieldOptionResourceCollection" />.
  /// </summary>
  public FormFieldOptionResourceCollection Options => GetRelated<FormFieldOptionResourceCollection>("options");

  internal FormFieldResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public FormFieldResource Include(params FormFieldIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People FormField resources.
/// </summary>
public class FormFieldResourceCollection
  : PlanningCenterPaginatedFetchableResource<FormField, FormFieldResourceCollection, FormFieldResource, PeopleDocumentContext>,
  IIncludable<FormFieldResourceCollection, FormFieldIncludable>,
  IOrderable<FormFieldResourceCollection, FormFieldOrderable>
{
  internal FormFieldResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public FormFieldResourceCollection Include(params FormFieldIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public FormFieldResourceCollection OrderBy(FormFieldOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

