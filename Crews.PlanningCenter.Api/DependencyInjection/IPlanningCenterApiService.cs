using Crews.PlanningCenter.Api.Clients;

namespace Crews.PlanningCenter.Api.DependencyInjection;

/// <summary>
/// Represents a service that provides clients for Planning Center product APIs.
/// </summary>
public interface IPlanningCenterApiService
{
	/// <summary>
	/// Planning Center Calendar API client.
	/// </summary>
	public CalendarClient Calendar { get; }
	
	/// <summary>
	/// Planning Center Check-Ins API client.
	/// </summary>
	public CheckInsClient CheckIns { get; }
	
	/// <summary>
	/// Planning Center Giving API client.
	/// </summary>
	public GivingClient Giving { get; }
	
	/// <summary>
	/// Planning Center Groups API client.
	/// </summary>
	public GroupsClient Groups { get; }
	
	/// <summary>
	/// Planning Center People API client.
	/// </summary>
	public PeopleClient People { get; }
	
	/// <summary>
	/// Planning Center Publishing API client.
	/// </summary>
	public PublishingClient Publishing { get; }
	
	/// <summary>
	/// Planning Center Services API client.
	/// </summary>
	public ServicesClient Services { get; }
}
