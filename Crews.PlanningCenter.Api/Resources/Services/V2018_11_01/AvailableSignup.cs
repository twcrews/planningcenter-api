/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_11_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_11_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_11_01;

/// <summary>
/// A fetchable Services AvailableSignup resource.
/// </summary>
public class AvailableSignupResource
  : PlanningCenterSingletonFetchableResource<AvailableSignup, AvailableSignupResource, ServicesDocumentContext>,
  IIncludable<AvailableSignupResource, AvailableSignupIncludable>
{

  /// <summary>
  /// The related <see cref="SignupSheetResourceCollection" />.
  /// </summary>
  public SignupSheetResourceCollection SignupSheets => GetRelated<SignupSheetResourceCollection>("signup_sheets");

  internal AvailableSignupResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public AvailableSignupResource Include(params AvailableSignupIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Services AvailableSignup resources.
/// </summary>
public class AvailableSignupResourceCollection
  : PlanningCenterPaginatedFetchableResource<AvailableSignup, AvailableSignupResourceCollection, AvailableSignupResource, ServicesDocumentContext>,
  IIncludable<AvailableSignupResourceCollection, AvailableSignupIncludable>
{
  internal AvailableSignupResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public AvailableSignupResourceCollection Include(params AvailableSignupIncludable[] included)
    => base.Include(included);
}

