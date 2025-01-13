/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2019_07_17.Entities;
using Crews.PlanningCenter.Models.CheckIns.V2019_07_17.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2019_07_17;

/// <summary>
/// A fetchable CheckIns Option resource.
/// </summary>
public class OptionResource
  : PlanningCenterSingletonFetchableResource<Option, OptionResource, CheckInsDocumentContext>,
  IIncludable<OptionResource, OptionIncludable>
{

  /// <summary>
  /// The related <see cref="CheckInResourceCollection" />.
  /// </summary>
  public CheckInResourceCollection CheckIns => GetRelated<CheckInResourceCollection>("check_ins");

  /// <summary>
  /// The related <see cref="LabelResource" />.
  /// </summary>
  public LabelResource Label => GetRelated<LabelResource>("label");

  internal OptionResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public OptionResource Include(params OptionIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of CheckIns Option resources.
/// </summary>
public class OptionResourceCollection
  : PlanningCenterPaginatedFetchableResource<Option, OptionResourceCollection, OptionResource, CheckInsDocumentContext>,
  IIncludable<OptionResourceCollection, OptionIncludable>
{
  internal OptionResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public OptionResourceCollection Include(params OptionIncludable[] included)
    => base.Include(included);
}

