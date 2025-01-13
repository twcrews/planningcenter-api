using System.Net.Http.Headers;

namespace Crews.PlanningCenter.Api.Extensions;

static class HttpHeadersExtensions
{
	public static void AddPlanningCenterVersion(this HttpHeaders headers, string version)
		=> headers.Add("X-PCO-API-Version", version);
}
