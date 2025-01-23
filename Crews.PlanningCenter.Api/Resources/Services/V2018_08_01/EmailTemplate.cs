/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services EmailTemplate resource.
/// </summary>
public class EmailTemplateResource
  : PlanningCenterSingletonFetchableResource<EmailTemplate, EmailTemplateResource, ServicesDocumentContext>
{

  internal EmailTemplateResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<EmailTemplate>> PostAsync(EmailTemplate resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<EmailTemplate>> PatchAsync(EmailTemplate resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Services EmailTemplate resources.
/// </summary>
public class EmailTemplateResourceCollection
  : PlanningCenterPaginatedFetchableResource<EmailTemplate, EmailTemplateResourceCollection, EmailTemplateResource, ServicesDocumentContext>
{
  internal EmailTemplateResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

