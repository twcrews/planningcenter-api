using System.Net.Http.Headers;
using Crews.Extensions.Primitives;
using Crews.PlanningCenter.Api.Authentication;

namespace Crews.PlanningCenter.Api.Tests.Models;

public class PlanningCenterPersonalAccessTokenTests
{
	[Fact(DisplayName = "Instance can be implicitly converted to the expected auth header value")]
	public void AuthenticationHeaderValue_ImplicitConversion()
	{
		PlanningCenterPersonalAccessToken token = new()
		{
			AppID = "testID",
			Secret = "testSecret"
		};

		AuthenticationHeaderValue authHeader = token;

		string expectedParam = $"{token.AppID}:{token.Secret}".Base64Encode();
		string? actualParam = authHeader.Parameter;
		
		Assert.Equal(expectedParam, actualParam);
	}
}
