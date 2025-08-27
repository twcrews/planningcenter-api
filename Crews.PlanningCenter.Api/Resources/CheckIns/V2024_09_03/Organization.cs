/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2024_09_03.Entities;
using Crews.PlanningCenter.Models.CheckIns.V2024_09_03.Parameters;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2024_09_03;

/// <summary>
/// A fetchable CheckIns Organization resource.
/// </summary>
public class OrganizationResource
  : PlanningCenterSingletonFetchableResource<Organization, OrganizationResource, CheckInsDocumentContext>
{

  /// <summary>
  /// The related <see cref="CheckInResourceCollection" />.
  /// </summary>
  public CheckInResourceCollection CheckIns => GetRelated<CheckInResourceCollection>("check_ins");

  /// <summary>
  /// The related <see cref="EventTimeResourceCollection" />.
  /// </summary>
  public EventTimeResourceCollection EventTimes => GetRelated<EventTimeResourceCollection>("event_times");

  /// <summary>
  /// The related <see cref="EventResourceCollection" />.
  /// </summary>
  public EventResourceCollection Events => GetRelated<EventResourceCollection>("events");

  /// <summary>
  /// The related <see cref="HeadcountResourceCollection" />.
  /// </summary>
  public HeadcountResourceCollection Headcounts => GetRelated<HeadcountResourceCollection>("headcounts");

  /// <summary>
  /// The related <see cref="IntegrationLinkResourceCollection" />.
  /// </summary>
  public IntegrationLinkResourceCollection IntegrationLinks => GetRelated<IntegrationLinkResourceCollection>("integration_links");

  /// <summary>
  /// The related <see cref="LabelResourceCollection" />.
  /// </summary>
  public LabelResourceCollection Labels => GetRelated<LabelResourceCollection>("labels");

  /// <summary>
  /// The related <see cref="OptionResourceCollection" />.
  /// </summary>
  public OptionResourceCollection Options => GetRelated<OptionResourceCollection>("options");

  /// <summary>
  /// The related <see cref="PassResourceCollection" />.
  /// </summary>
  public PassResourceCollection Passes => GetRelated<PassResourceCollection>("passes");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource People => GetRelated<PersonResource>("people");

  /// <summary>
  /// The related <see cref="StationResourceCollection" />.
  /// </summary>
  public StationResourceCollection Stations => GetRelated<StationResourceCollection>("stations");

  /// <summary>
  /// The related <see cref="ThemeResourceCollection" />.
  /// </summary>
  public ThemeResourceCollection Themes => GetRelated<ThemeResourceCollection>("themes");

  internal OrganizationResource(Uri uri, HttpClient client) : base(uri, client) { }
}


