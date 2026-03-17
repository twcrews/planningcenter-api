using Crews.PlanningCenter.Api.Models;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.Api.V2018_08_01;

/// <summary>
/// Client for interacting with the Organization resource.
/// </summary>
public class OrganizationClient(HttpClient httpClient, Uri uri) : SingletonResourceClient<Organization, OrganizationResource, OrganizationResourceResponse>(httpClient, uri)
{
    /// <summary>
    /// Associated <c>ConnectedApplications</c>.
    /// </summary>
    public PaginatedConnectedApplicationClient ConnectedApplications => new(HttpClient, new(Uri, "connected_applications/"));
}

/// <summary>
/// Attributes for the Organization resource.
/// </summary>
public record Organization { }

/// <summary>
/// Planning Center does not provide a description for this resource.
/// </summary>
public record OrganizationResource : JsonApiResource<Organization> { }

/// <summary>
/// Response model for Organization resource.
/// </summary>
public class OrganizationResourceResponse : ResourceResponse<OrganizationResource> { }