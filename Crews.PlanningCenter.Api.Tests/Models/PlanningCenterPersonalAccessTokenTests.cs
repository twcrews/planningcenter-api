using System.Net.Http.Headers;
using System.Text;
using Crews.PlanningCenter.Api.Authentication;

namespace Crews.PlanningCenter.Api.Tests.Models;

public class PlanningCenterPersonalAccessTokenTests
{
	[Fact(DisplayName = "Instance can be implicitly converted to the expected auth header value")]
	public void AuthenticationHeaderValue_ImplicitConversion()
	{
		PlanningCenterPersonalAccessToken token = new()
		{
			AppId = "testID",
			Secret = "testSecret"
		};

		AuthenticationHeaderValue authHeader = token;

		string expectedParam = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{token.AppId}:{token.Secret}"));
		string? actualParam = authHeader.Parameter;
		
		Assert.Equal(expectedParam, actualParam);
	}
}
