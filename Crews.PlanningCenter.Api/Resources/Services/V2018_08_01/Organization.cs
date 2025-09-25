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
/// A fetchable Services Organization resource.
/// </summary>
public class OrganizationResource
  : PlanningCenterSingletonFetchableResource<Organization, OrganizationResource, ServicesDocumentContext>
{

  /// <summary>
  /// The related <see cref="AttachmentTypeResourceCollection" />.
  /// </summary>
  public AttachmentTypeResourceCollection AttachmentTypes => GetRelated<AttachmentTypeResourceCollection>("attachment_types");

  /// <summary>
  /// The related <see cref="ChatResource" />.
  /// </summary>
  public ChatResource Chat => GetRelated<ChatResource>("chat");

  /// <summary>
  /// The related <see cref="EmailTemplateResourceCollection" />.
  /// </summary>
  public EmailTemplateResourceCollection EmailTemplates => GetRelated<EmailTemplateResourceCollection>("email_templates");

  /// <summary>
  /// The related <see cref="FolderResourceCollection" />.
  /// </summary>
  public FolderResourceCollection Folders => GetRelated<FolderResourceCollection>("folders");

  /// <summary>
  /// The related <see cref="MediaResource" />.
  /// </summary>
  public MediaResource Media => GetRelated<MediaResource>("media");

  /// <summary>
  /// The related <see cref="PersonResourceCollection" />.
  /// </summary>
  public PersonResourceCollection People => GetRelated<PersonResourceCollection>("people");

  /// <summary>
  /// The related <see cref="PlanResourceCollection" />.
  /// </summary>
  public PlanResourceCollection Plans => GetRelated<PlanResourceCollection>("plans");

  /// <summary>
  /// The related <see cref="ReportTemplateResourceCollection" />.
  /// </summary>
  public ReportTemplateResourceCollection ReportTemplates => GetRelated<ReportTemplateResourceCollection>("report_templates");

  /// <summary>
  /// The related <see cref="SeriesResourceCollection" />.
  /// </summary>
  public SeriesResourceCollection Series => GetRelated<SeriesResourceCollection>("series");

  /// <summary>
  /// The related <see cref="ServiceTypeResourceCollection" />.
  /// </summary>
  public ServiceTypeResourceCollection ServiceTypes => GetRelated<ServiceTypeResourceCollection>("service_types");

  /// <summary>
  /// The related <see cref="SongResourceCollection" />.
  /// </summary>
  public SongResourceCollection Songs => GetRelated<SongResourceCollection>("songs");

  /// <summary>
  /// The related <see cref="TagGroupResourceCollection" />.
  /// </summary>
  public TagGroupResourceCollection TagGroups => GetRelated<TagGroupResourceCollection>("tag_groups");

  /// <summary>
  /// The related <see cref="TeamResourceCollection" />.
  /// </summary>
  public TeamResourceCollection Teams => GetRelated<TeamResourceCollection>("teams");

  internal OrganizationResource(Uri uri, HttpClient client) : base(uri, client) { }
}


