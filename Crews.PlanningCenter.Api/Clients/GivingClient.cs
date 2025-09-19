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
/// Service for interacting with Planning Center Giving APIs.
/// </summary>
/// <param name="client">The <see cref="HttpClient"/> used to send requests.</param>
/// <param name="baseUri">
/// An optional base <see cref="Uri"/> to use for the API address. If omitted,
/// <see cref="HttpClient.BaseAddress"/> is used instead. If this is also not set,
/// the default Planning Center API base address as defined in 
/// <see cref="PlanningCenterApiOptions.DefaultPlanningCenterApiBaseAddress"/> is used.
/// </param>
public class GivingClient(HttpClient client, Uri? baseUri = null)
{
  private readonly HttpClient _client = client;
  private readonly Uri _baseUri = baseUri ?? client.BaseAddress 
    ?? new(PlanningCenterApiOptions.DefaultPlanningCenterApiBaseAddress);
  /// <summary>
  /// Gets a client for the latest version of the Giving API.
  /// </summary>
  public Resources.Giving.V2019_10_18.OrganizationResource LatestVersion => V2019_10_18;

  /// <summary>
  /// Gets a client for version 2019-10-18 of the Giving API.
  /// </summary>
  public Resources.Giving.V2019_10_18.OrganizationResource V2019_10_18
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2019-10-18");
      return new(_baseUri.SafelyAppendPath("giving/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2018-08-01 of the Giving API.
  /// </summary>
  public Resources.Giving.V2018_08_01.OrganizationResource V2018_08_01
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2018-08-01");
      return new(_baseUri.SafelyAppendPath("giving/v2"), _client);
    }
  }
}

