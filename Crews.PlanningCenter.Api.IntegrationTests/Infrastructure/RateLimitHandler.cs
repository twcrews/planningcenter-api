using System.Net;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;

/// <summary>
/// Delegating handler that proactively throttles requests to avoid Planning Center's API rate
/// limits, and retries on 429 responses. Limit and period are read dynamically from the
/// <c>X-PCO-API-Request-Rate-Limit</c> and <c>X-PCO-API-Request-Rate-Period</c> response headers.
/// </summary>
/// <remarks>
/// Shared static state ensures all fixture instances (across product collections that may run in
/// parallel) are throttled against a single window.
/// </remarks>
public class RateLimitHandler : DelegatingHandler
{
	private static readonly object _lock = new();
	private static int _requestsInWindow = 0;
	private static int _dynamicLimit = 100;
	private static int _dynamicPeriodSeconds = 20;
	private static DateTimeOffset _windowStart = DateTimeOffset.UtcNow;

	protected override async Task<HttpResponseMessage> SendAsync(
		HttpRequestMessage request, CancellationToken cancellationToken)
	{
		// Buffer request body so it can be reconstructed on retry (HttpContent is single-use).
		byte[]? bodyBytes = null;
		System.Net.Http.Headers.MediaTypeHeaderValue? contentType = null;
		if (request.Content is not null)
		{
			bodyBytes = await request.Content.ReadAsByteArrayAsync(cancellationToken);
			contentType = request.Content.Headers.ContentType;
		}

		while (true)
		{
			await ProactiveThrottleAsync(cancellationToken);

			if (bodyBytes is not null)
			{
				request.Content = new ByteArrayContent(bodyBytes);
				if (contentType is not null)
					request.Content.Headers.ContentType = contentType;
			}

			var response = await base.SendAsync(request, cancellationToken);
			UpdateFromHeaders(response);

			if (response.StatusCode != HttpStatusCode.TooManyRequests)
				return response;

			// 429 — wait the server-specified backoff before retrying.
			var retryAfter = response.Headers.RetryAfter?.Delta
				?? TimeSpan.FromSeconds(_dynamicPeriodSeconds);
			await Task.Delay(retryAfter, cancellationToken);

			lock (_lock)
			{
				_requestsInWindow = 0;
				_windowStart = DateTimeOffset.UtcNow;
			}
		}
	}

	private static async Task ProactiveThrottleAsync(CancellationToken cancellationToken)
	{
		while (true)
		{
			TimeSpan waitTime;

			lock (_lock)
			{
				var now = DateTimeOffset.UtcNow;
				var elapsed = now - _windowStart;

				if (elapsed >= TimeSpan.FromSeconds(_dynamicPeriodSeconds))
				{
					// The window has rolled over — reset the local counter.
					_windowStart = now;
					_requestsInWindow = 0;
				}

				if (_requestsInWindow < _dynamicLimit)
				{
					_requestsInWindow++;
					return;
				}

				// At the limit — compute remaining time in the current window.
				waitTime = TimeSpan.FromSeconds(_dynamicPeriodSeconds) - elapsed;
			}

			if (waitTime > TimeSpan.Zero)
				await Task.Delay(waitTime, cancellationToken);
			// Loop back: the window should have reset by now, so the next check will pass.
		}
	}

	private static void UpdateFromHeaders(HttpResponseMessage response)
	{
		if (response.Headers.TryGetValues("X-PCO-API-Request-Rate-Limit", out var limitVals)
			&& int.TryParse(limitVals.FirstOrDefault(), out var limit))
			Interlocked.Exchange(ref _dynamicLimit, limit);

		if (response.Headers.TryGetValues("X-PCO-API-Request-Rate-Period", out var periodVals))
		{
			// Header value is formatted as "20 seconds".
			var parts = periodVals.FirstOrDefault()?.Split(' ');
			if (parts is [var numStr, ..] && int.TryParse(numStr, out var secs))
				Interlocked.Exchange(ref _dynamicPeriodSeconds, secs);
		}
	}
}
