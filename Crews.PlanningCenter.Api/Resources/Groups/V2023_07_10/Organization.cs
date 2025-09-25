/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Groups.V2023_07_10.Entities;
using Crews.PlanningCenter.Models.Groups.V2023_07_10.Parameters;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Groups.V2023_07_10;

/// <summary>
/// A fetchable Groups Organization resource.
/// </summary>
public class OrganizationResource
  : PlanningCenterSingletonFetchableResource<Organization, OrganizationResource, GroupsDocumentContext>
{

  /// <summary>
  /// The related <see cref="CampusResourceCollection" />.
  /// </summary>
  public CampusResourceCollection Campuses => GetRelated<CampusResourceCollection>("campuses");

  /// <summary>
  /// The related <see cref="EventResourceCollection" />.
  /// </summary>
  public EventResourceCollection Events => GetRelated<EventResourceCollection>("events");

  /// <summary>
  /// The related <see cref="GroupApplicationResourceCollection" />.
  /// </summary>
  public GroupApplicationResourceCollection GroupApplications => GetRelated<GroupApplicationResourceCollection>("group_applications");

  /// <summary>
  /// The related <see cref="GroupTypeResourceCollection" />.
  /// </summary>
  public GroupTypeResourceCollection GroupTypes => GetRelated<GroupTypeResourceCollection>("group_types");

  /// <summary>
  /// The related <see cref="GroupResourceCollection" />.
  /// </summary>
  public GroupResourceCollection Groups => GetRelated<GroupResourceCollection>("groups");

  /// <summary>
  /// The related <see cref="PersonResourceCollection" />.
  /// </summary>
  public PersonResourceCollection People => GetRelated<PersonResourceCollection>("people");

  /// <summary>
  /// The related <see cref="TagGroupResourceCollection" />.
  /// </summary>
  public TagGroupResourceCollection TagGroups => GetRelated<TagGroupResourceCollection>("tag_groups");

  internal OrganizationResource(Uri uri, HttpClient client) : base(uri, client) { }
}


