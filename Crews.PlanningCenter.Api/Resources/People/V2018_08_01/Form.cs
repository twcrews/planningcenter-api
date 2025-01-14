/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.People.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2018_08_01;

/// <summary>
/// A fetchable People Form resource.
/// </summary>
public class FormResource
  : PlanningCenterSingletonFetchableResource<Form, FormResource, PeopleDocumentContext>,
  IIncludable<FormResource, FormIncludable>
{

  /// <summary>
  /// The related <see cref="CampusResource" />.
  /// </summary>
  public CampusResource Campus => GetRelated<CampusResource>("campus");

  /// <summary>
  /// The related <see cref="FormFieldResourceCollection" />.
  /// </summary>
  public FormFieldResourceCollection Fields => GetRelated<FormFieldResourceCollection>("fields");

  /// <summary>
  /// The related <see cref="FormSubmissionResourceCollection" />.
  /// </summary>
  public FormSubmissionResourceCollection FormSubmissions => GetRelated<FormSubmissionResourceCollection>("form_submissions");

  internal FormResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public FormResource Include(params FormIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People Form resources.
/// </summary>
public class FormResourceCollection
  : PlanningCenterPaginatedFetchableResource<Form, FormResourceCollection, FormResource, PeopleDocumentContext>,
  IIncludable<FormResourceCollection, FormIncludable>,
  IOrderable<FormResourceCollection, FormOrderable>,
  IQueryable<FormResourceCollection, FormQueryable>
{
  internal FormResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public FormResourceCollection Include(params FormIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public FormResourceCollection OrderBy(FormOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public FormResourceCollection Query(params (FormQueryable, string)[] queries)
    => base.Query(queries);
}

