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
/// A fetchable People FormSubmission resource.
/// </summary>
public class FormSubmissionResource
  : PlanningCenterSingletonFetchableResource<FormSubmission, FormSubmissionResource, PeopleDocumentContext>,
  IIncludable<FormSubmissionResource, FormSubmissionIncludable>
{

  /// <summary>
  /// The related <see cref="FormResource" />.
  /// </summary>
  public FormResource Form => GetRelated<FormResource>("form");

  /// <summary>
  /// The related <see cref="FormFieldResourceCollection" />.
  /// </summary>
  public FormFieldResourceCollection FormFields => GetRelated<FormFieldResourceCollection>("form_fields");

  /// <summary>
  /// The related <see cref="FormSubmissionValueResourceCollection" />.
  /// </summary>
  public FormSubmissionValueResourceCollection FormSubmissionValues => GetRelated<FormSubmissionValueResourceCollection>("form_submission_values");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal FormSubmissionResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public FormSubmissionResource Include(params FormSubmissionIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People FormSubmission resources.
/// </summary>
public class FormSubmissionResourceCollection
  : PlanningCenterPaginatedFetchableResource<FormSubmission, FormSubmissionResourceCollection, FormSubmissionResource, PeopleDocumentContext>,
  IIncludable<FormSubmissionResourceCollection, FormSubmissionIncludable>,
  IOrderable<FormSubmissionResourceCollection, FormSubmissionOrderable>
{
  internal FormSubmissionResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public FormSubmissionResourceCollection Include(params FormSubmissionIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public FormSubmissionResourceCollection OrderBy(FormSubmissionOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

