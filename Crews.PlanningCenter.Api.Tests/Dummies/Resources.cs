﻿using Crews.PlanningCenter.Api.Models;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel.Configuration;

namespace Crews.PlanningCenter.Api.Tests.Dummies;

class DummyFetchableResource(Uri? uri, HttpClient client) :
	PlanningCenterFetchableResource<DummyFetchableResource>(uri!, client),
	IIncludable<DummyFetchableResource, DummyEnum>
{

	public DummyPaginatedFetchableResource Dummies => GetRelated<DummyPaginatedFetchableResource>("dummies");
	public DummySingletonFetchableResource Dummy => GetRelated<DummySingletonFetchableResource>("dummy");

	public DummyFetchableResource Include(params DummyEnum[] includables) => base.Include(includables);
	public new DummyFetchableResource AddParameters(string key, params string[] values)
		=> base.AddParameters(key, values);

	public new Task<Document?> GetDocumentAsync(HttpResponseMessage response) => base.GetDocumentAsync(response);
}

class DummyContext : PlanningCenterDocumentContext
{
	public DummyContext() : base() { }
	public DummyContext(Document document) : base(document) { }

	protected override void OnServiceModelCreating(IServiceModelBuilder serviceModelBuilder)
	{
		base.OnServiceModelCreating(serviceModelBuilder);
		serviceModelBuilder.Configurations.Add(new DummyResourceConfiguration());
	}
}

class DummySingletonFetchableResource(Uri? uri, HttpClient client) :
	PlanningCenterSingletonFetchableResource<
		DummyResource,
		DummySingletonFetchableResource,
		DummyContext>(uri!, client)
{
	public new Task<JsonApiSingletonResponse<DummyResource>> PostAsync(DummyResource resource) => base.PostAsync(resource);
	public new Task<JsonApiSingletonResponse<DummyResource>> PatchAsync(DummyResource resource) => base.PatchAsync(resource);
	public new Task DeleteAsync() => base.DeleteAsync();
}

class DummyPaginatedFetchableResource(Uri? uri, HttpClient client) :
	PlanningCenterPaginatedFetchableResource<
		DummyResource,
		DummyPaginatedFetchableResource,
		DummySingletonFetchableResource,
		DummyContext>(uri!, client),
	IFilterable<DummyPaginatedFetchableResource, DummyEnum>,
	IOrderable<DummyPaginatedFetchableResource, DummyEnum>,
	IQueryable<DummyPaginatedFetchableResource, DummyEnum>
{
	public DummyPaginatedFetchableResource FilterBy(params DummyEnum[] filters) => base.FilterBy(filters);
	public DummyPaginatedFetchableResource OrderBy(DummyEnum orderer, Order order = Order.Ascending) 
		=> base.OrderBy(orderer, order);
	public DummyPaginatedFetchableResource Query(params (DummyEnum, string)[] queries) => base.Query(queries);
}

class DummyResource
{
	public required string ID { get; set; }
	public string? Name { get; set; }
	public int Age { get; set; }
}