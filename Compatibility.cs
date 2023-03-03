using System;
using System.Collections.Generic;
using System.Text;

namespace Loxifi
{
#if NETSTANDARD2_0
	public static class HttpClientExtensions
	{
		public static async Task<HttpResponseMessage> PatchAsync(this HttpClient httpClient, string requestUri, HttpContent content, CancellationToken cancellationToken) => await httpClient.PatchAsync(new Uri(requestUri), content, cancellationToken);

		public static async Task<HttpResponseMessage> PatchAsync(this HttpClient httpClient, string requestUri, HttpContent content) => await httpClient.PatchAsync(new Uri(requestUri), content, CancellationToken.None);
		public static async Task<HttpResponseMessage> PatchAsync(this HttpClient httpClient, Uri requestUri, HttpContent content) => await httpClient.PatchAsync(requestUri, content, CancellationToken.None);

		public static async Task<HttpResponseMessage> PatchAsync(this HttpClient httpClient, Uri requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			string method = "PATCH";
			HttpMethod httpVerb = new(method);
			HttpRequestMessage httpRequestMessage =
				new(httpVerb, requestUri)
				{
					Content = content
				};

			return await httpClient.SendAsync(httpRequestMessage);
		}
	}
#endif
}
