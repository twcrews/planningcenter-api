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
/// A fetchable Services SignupSheet resource.
/// </summary>
public class SignupSheetResource
  : PlanningCenterSingletonFetchableResource<SignupSheet, SignupSheetResource, ServicesDocumentContext>,
  IIncludable<SignupSheetResource, SignupSheetIncludable>
{

  /// <summary>
  /// The related <see cref="ScheduledPersonResource" />.
  /// </summary>
  public ScheduledPersonResource ScheduledPeople => GetRelated<ScheduledPersonResource>("scheduled_people");

  /// <summary>
  /// The related <see cref="SignupSheetMetadataResource" />.
  /// </summary>
  public SignupSheetMetadataResource SignupSheetMetadata => GetRelated<SignupSheetMetadataResource>("signup_sheet_metadata");

  internal SignupSheetResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public SignupSheetResource Include(params SignupSheetIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Services SignupSheet resources.
/// </summary>
public class SignupSheetResourceCollection
  : PlanningCenterPaginatedFetchableResource<SignupSheet, SignupSheetResourceCollection, SignupSheetResource, ServicesDocumentContext>,
  IIncludable<SignupSheetResourceCollection, SignupSheetIncludable>
{
  internal SignupSheetResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public SignupSheetResourceCollection Include(params SignupSheetIncludable[] included)
    => base.Include(included);
}

