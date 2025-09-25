/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.Extensions.Http;
using Crews.PlanningCenter.Api.Extensions;
using Crews.PlanningCenter.Api.DependencyInjection;

namespace Crews.PlanningCenter.Api.Clients;

/// <summary>
/// Service for interacting with Planning Center Groups APIs.
/// </summary>
/// <param name="client">The <see cref="HttpClient"/> used to send requests.</param>
/// <param name="baseUri">
/// An optional base <see cref="Uri"/> to use for the API address. If omitted,
/// <see cref="HttpClient.BaseAddress"/> is used instead. If this is also not set,
/// the default Planning Center API base address as defined in 
/// <see cref="PlanningCenterApiOptions.DefaultPlanningCenterApiBaseAddress"/> is used.
/// </param>
public class GroupsClient(HttpClient client, Uri? baseUri = null)
{
  private readonly HttpClient _client = client;
  private readonly Uri _baseUri = baseUri ?? client.BaseAddress 
    ?? new(PlanningCenterApiOptions.DefaultPlanningCenterApiBaseAddress);
  /// <summary>
  /// Gets a client for the latest version of the Groups API.
  /// </summary>
  public Resources.Groups.V2023_07_10.OrganizationResource LatestVersion => V2023_07_10;

  /// <summary>
  /// Gets a client for version 2023-07-10 of the Groups API.
  /// </summary>
  public Resources.Groups.V2023_07_10.OrganizationResource V2023_07_10
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2023-07-10");
      return new(_baseUri.SafelyAppendPath("groups/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2018-08-01 of the Groups API.
  /// </summary>
  public Resources.Groups.V2018_08_01.OrganizationResource V2018_08_01
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2018-08-01");
      return new(_baseUri.SafelyAppendPath("groups/v2"), _client);
    }
  }
}

