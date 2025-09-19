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
/// Service for interacting with Planning Center People APIs.
/// </summary>
/// <param name="client">The <see cref="HttpClient"/> used to send requests.</param>
/// <param name="baseUri">
/// An optional base <see cref="Uri"/> to use for the API address. If omitted,
/// <see cref="HttpClient.BaseAddress"/> is used instead. If this is also not set,
/// the default Planning Center API base address as defined in 
/// <see cref="PlanningCenterApiOptions.DefaultPlanningCenterApiBaseAddress"/> is used.
/// </param>
public class PeopleClient(HttpClient client, Uri? baseUri = null)
{
  private readonly HttpClient _client = client;
  private readonly Uri _baseUri = baseUri ?? client.BaseAddress 
    ?? new(PlanningCenterApiOptions.DefaultPlanningCenterApiBaseAddress);
  /// <summary>
  /// Gets a client for the latest version of the People API.
  /// </summary>
  public Resources.People.V2025_07_17.OrganizationResource LatestVersion => V2025_07_17;

  /// <summary>
  /// Gets a client for version 2025-07-17 of the People API.
  /// </summary>
  public Resources.People.V2025_07_17.OrganizationResource V2025_07_17
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2025-07-17");
      return new(_baseUri.SafelyAppendPath("people/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2025-07-02 of the People API.
  /// </summary>
  public Resources.People.V2025_07_02.OrganizationResource V2025_07_02
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2025-07-02");
      return new(_baseUri.SafelyAppendPath("people/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2025-03-20 of the People API.
  /// </summary>
  public Resources.People.V2025_03_20.OrganizationResource V2025_03_20
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2025-03-20");
      return new(_baseUri.SafelyAppendPath("people/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2024-09-12 of the People API.
  /// </summary>
  public Resources.People.V2024_09_12.OrganizationResource V2024_09_12
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2024-09-12");
      return new(_baseUri.SafelyAppendPath("people/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2023-03-21 of the People API.
  /// </summary>
  public Resources.People.V2023_03_21.OrganizationResource V2023_03_21
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2023-03-21");
      return new(_baseUri.SafelyAppendPath("people/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2023-02-15 of the People API.
  /// </summary>
  public Resources.People.V2023_02_15.OrganizationResource V2023_02_15
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2023-02-15");
      return new(_baseUri.SafelyAppendPath("people/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2022-07-14 of the People API.
  /// </summary>
  public Resources.People.V2022_07_14.OrganizationResource V2022_07_14
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2022-07-14");
      return new(_baseUri.SafelyAppendPath("people/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2022-01-28 of the People API.
  /// </summary>
  public Resources.People.V2022_01_28.OrganizationResource V2022_01_28
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2022-01-28");
      return new(_baseUri.SafelyAppendPath("people/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2022-01-05 of the People API.
  /// </summary>
  public Resources.People.V2022_01_05.OrganizationResource V2022_01_05
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2022-01-05");
      return new(_baseUri.SafelyAppendPath("people/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2021-08-17 of the People API.
  /// </summary>
  public Resources.People.V2021_08_17.OrganizationResource V2021_08_17
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2021-08-17");
      return new(_baseUri.SafelyAppendPath("people/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2020-07-22 of the People API.
  /// </summary>
  public Resources.People.V2020_07_22.OrganizationResource V2020_07_22
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2020-07-22");
      return new(_baseUri.SafelyAppendPath("people/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2020-04-06 of the People API.
  /// </summary>
  public Resources.People.V2020_04_06.OrganizationResource V2020_04_06
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2020-04-06");
      return new(_baseUri.SafelyAppendPath("people/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2019-10-10 of the People API.
  /// </summary>
  public Resources.People.V2019_10_10.OrganizationResource V2019_10_10
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2019-10-10");
      return new(_baseUri.SafelyAppendPath("people/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2019-01-14 of the People API.
  /// </summary>
  public Resources.People.V2019_01_14.OrganizationResource V2019_01_14
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2019-01-14");
      return new(_baseUri.SafelyAppendPath("people/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2018-08-01 of the People API.
  /// </summary>
  public Resources.People.V2018_08_01.OrganizationResource V2018_08_01
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2018-08-01");
      return new(_baseUri.SafelyAppendPath("people/v2"), _client);
    }
  }
}

