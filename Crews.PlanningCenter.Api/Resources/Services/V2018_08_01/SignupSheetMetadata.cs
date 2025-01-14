/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services SignupSheetMetadata resource.
/// </summary>
public class SignupSheetMetadataResource
  : PlanningCenterSingletonFetchableResource<SignupSheetMetadata, SignupSheetMetadataResource, ServicesDocumentContext>
{

  internal SignupSheetMetadataResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services SignupSheetMetadata resources.
/// </summary>
public class SignupSheetMetadataResourceCollection
  : PlanningCenterPaginatedFetchableResource<SignupSheetMetadata, SignupSheetMetadataResourceCollection, SignupSheetMetadataResource, ServicesDocumentContext>
{
  internal SignupSheetMetadataResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

