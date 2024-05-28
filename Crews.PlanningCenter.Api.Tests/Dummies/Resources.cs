using Crews.PlanningCenter.Api.Models.Resources.PlanningCenter;
using Crews.PlanningCenter.Api.Models.Resources.Querying;

namespace Crews.PlanningCenter.Api.Tests;

class DummyFetchableResource(Uri? uri) : 
	PlanningCenterFetchableResource<DummyFetchableResource>(uri!),
	IIncludable<DummyFetchableResource, DummyEnum> 
{ 
	public DummyFetchableResource Include(params DummyEnum[] includables) => base.Include(includables);
}

class DummyContext : PlanningCenterDocumentContext { }

class DummySingletonFetchableResource(Uri? uri) :
	PlanningCenterSingletonFetchableResource<
		DummyResource, 
		DummySingletonFetchableResource, 
		DummyContext>(uri!) { }

class DummyPaginatedFetchableResource(Uri? uri) :
	PlanningCenterPaginatedFetchableResource<
		DummyResource, 
		DummyPaginatedFetchableResource, 
		DummySingletonFetchableResource, 
		DummyContext>(uri!),
	IFilterable<DummyPaginatedFetchableResource, DummyEnum>,
	IOrderable<DummyPaginatedFetchableResource, DummyEnum>,
	IQueryable<DummyPaginatedFetchableResource, DummyEnum>
{
	public DummyPaginatedFetchableResource FilterBy(params DummyEnum[] filters) => base.FilterBy(filters);
	public DummyPaginatedFetchableResource OrderBy(DummyEnum orderer) => base.OrderBy(orderer);
	public DummyPaginatedFetchableResource Query(params KeyValuePair<DummyEnum, string>[] queries) => base.Query(queries);
}

class DummyResource
{
	public required string ID { get; set; }
	public string? Name { get; set; }
	public int Age { get; set; }
}