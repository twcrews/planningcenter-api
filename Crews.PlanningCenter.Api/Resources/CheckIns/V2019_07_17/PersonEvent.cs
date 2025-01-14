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
/// A fetchable CheckIns PersonEvent resource.
/// </summary>
public class PersonEventResource
  : PlanningCenterSingletonFetchableResource<PersonEvent, PersonEventResource, CheckInsDocumentContext>,
  IIncludable<PersonEventResource, PersonEventIncludable>
{

  /// <summary>
  /// The related <see cref="EventResource" />.
  /// </summary>
  public EventResource Event => GetRelated<EventResource>("event");

  /// <summary>
  /// The related <see cref="CheckInResource" />.
  /// </summary>
  public CheckInResource FirstCheckIn => GetRelated<CheckInResource>("first_check_in");

  /// <summary>
  /// The related <see cref="CheckInResource" />.
  /// </summary>
  public CheckInResource LastCheckIn => GetRelated<CheckInResource>("last_check_in");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal PersonEventResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public PersonEventResource Include(params PersonEventIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of CheckIns PersonEvent resources.
/// </summary>
public class PersonEventResourceCollection
  : PlanningCenterPaginatedFetchableResource<PersonEvent, PersonEventResourceCollection, PersonEventResource, CheckInsDocumentContext>,
  IIncludable<PersonEventResourceCollection, PersonEventIncludable>
{
  internal PersonEventResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PersonEventResourceCollection Include(params PersonEventIncludable[] included)
    => base.Include(included);
}

