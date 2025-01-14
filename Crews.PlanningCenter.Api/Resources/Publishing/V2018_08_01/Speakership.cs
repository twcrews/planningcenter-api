/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Publishing.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Publishing.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Publishing.V2018_08_01;

/// <summary>
/// A fetchable Publishing Speakership resource.
/// </summary>
public class SpeakershipResource
  : PlanningCenterSingletonFetchableResource<Speakership, SpeakershipResource, PublishingDocumentContext>,
  IIncludable<SpeakershipResource, SpeakershipIncludable>
{

  /// <summary>
  /// The related <see cref="SpeakerResource" />.
  /// </summary>
  public SpeakerResource Speaker => GetRelated<SpeakerResource>("speaker");

  internal SpeakershipResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public SpeakershipResource Include(params SpeakershipIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Publishing Speakership resources.
/// </summary>
public class SpeakershipResourceCollection
  : PlanningCenterPaginatedFetchableResource<Speakership, SpeakershipResourceCollection, SpeakershipResource, PublishingDocumentContext>,
  IIncludable<SpeakershipResourceCollection, SpeakershipIncludable>
{
  internal SpeakershipResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public SpeakershipResourceCollection Include(params SpeakershipIncludable[] included)
    => base.Include(included);
}

