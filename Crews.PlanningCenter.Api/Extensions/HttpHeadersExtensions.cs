using System.Net.Http.Headers;

namespace Crews.PlanningCenter.Api.Extensions;

static class HttpHeadersExtensions
{
	public static void SetPlanningCenterVersion(this HttpHeaders headers, string version)
	{
		headers.Remove("X-PCO-API-Version");
		headers.Add("X-PCO-API-Version", version);
	}
}
