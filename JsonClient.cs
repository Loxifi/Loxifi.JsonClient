using Loxifi.Exceptions;
using Loxifi.JsonClient.Interfaces;
using System.Text;

namespace Loxifi.JsonClient
{
	public class JsonClient
	{
		public JsonClient(JsonClientSettings jsonClientSettings)
		{
			HttpClient = jsonClientSettings.HttpClient;
			JsonSerializer = jsonClientSettings.JsonSerializer;
			JsonSerializerSettings = jsonClientSettings.JsonSerializerSettings;
		}

		public JsonClient() : this(new JsonClientSettings())
		{
		}

		public HttpClient HttpClient { get; private set; }

		public IJsonSerializer JsonSerializer { get; private set; }

		public JsonSerializerSettings JsonSerializerSettings { get; private set; }

		public async Task<TOut> GetJsonAsync<TOut>(string requestUri, CancellationToken cancellationToken) => await ReadResponse<TOut>(HttpClient.GetAsync(requestUri, cancellationToken));

		public async Task<TOut> GetJsonAsync<TOut>(Uri requestUri) => await ReadResponse<TOut>(HttpClient.GetAsync(requestUri));

		public async Task<TOut> GetJsonAsync<TOut>(Uri requestUri, HttpCompletionOption completionOption) => await ReadResponse<TOut>(HttpClient.GetAsync(requestUri, completionOption));

		public async Task<TOut> GetJsonAsync<TOut>(string requestUri, HttpCompletionOption completionOption) => await ReadResponse<TOut>(HttpClient.GetAsync(requestUri, completionOption));

		public async Task<TOut> GetJsonAsync<TOut>(Uri requestUri, CancellationToken cancellationToken) => await ReadResponse<TOut>(HttpClient.GetAsync(requestUri, cancellationToken));

		public async Task<TOut> GetJsonAsync<TOut>(string requestUri) => await ReadResponse<TOut>(HttpClient.GetAsync(requestUri));

		public async Task<TOut> PatchJsonAsync<TOut>(Uri requestUri, object content) => await ReadResponse<TOut>(HttpClient.PatchAsync(requestUri, BuildContent(content)));

		public async Task<TOut> PatchJsonAsync<TOut>(string requestUri, object content) => await ReadResponse<TOut>(HttpClient.PatchAsync(requestUri, BuildContent(content)));

		public async Task<TOut> PatchJsonAsync<TOut>(string requestUri, object content, CancellationToken cancellationToken) => await ReadResponse<TOut>(HttpClient.PatchAsync(requestUri, BuildContent(content), cancellationToken));

		public async Task<TOut> PatchJsonAsync<TOut>(Uri requestUri, object content, CancellationToken cancellationToken) => await ReadResponse<TOut>(HttpClient.PatchAsync(requestUri, BuildContent(content), cancellationToken));

		public async Task PatchJsonAsync(Uri requestUri, object content) => await HttpClient.PatchAsync(requestUri, BuildContent(content));

		public async Task PatchJsonAsync(string requestUri, object content) => await HttpClient.PatchAsync(requestUri, BuildContent(content));

		public async Task PatchJsonAsync(string requestUri, object content, CancellationToken cancellationToken) => await HttpClient.PatchAsync(requestUri, BuildContent(content), cancellationToken);

		public async Task PatchJsonAsync(Uri requestUri, object content, CancellationToken cancellationToken) => await HttpClient.PatchAsync(requestUri, BuildContent(content), cancellationToken);

		public async Task<TOut> PostJsonAsync<TOut>(Uri requestUri, object content) => await ReadResponse<TOut>(HttpClient.PostAsync(requestUri, BuildContent(content)));

		public async Task<TOut> PostJsonAsync<TOut>(Uri requestUri, object content, CancellationToken cancellationToken) => await ReadResponse<TOut>(HttpClient.PostAsync(requestUri, BuildContent(content), cancellationToken));

		public async Task<TOut> PostJsonAsync<TOut>(string requestUri, object content) => await ReadResponse<TOut>(HttpClient.PostAsync(requestUri, BuildContent(content)));

		public async Task<TOut> PostJsonAsync<TOut>(string requestUri, object content, CancellationToken cancellationToken) => await ReadResponse<TOut>(HttpClient.PostAsync(requestUri, BuildContent(content), cancellationToken));

		public async Task PostJsonAsync(Uri requestUri, object content) => await HttpClient.PostAsync(requestUri, BuildContent(content));

		public async Task PostJsonAsync(Uri requestUri, object content, CancellationToken cancellationToken) => await HttpClient.PostAsync(requestUri, BuildContent(content), cancellationToken);

		public async Task PostJsonAsync(string requestUri, object content) => await HttpClient.PostAsync(requestUri, BuildContent(content));

		public async Task PostJsonAsync(string requestUri, object content, CancellationToken cancellationToken) => await HttpClient.PostAsync(requestUri, BuildContent(content), cancellationToken);

		public async Task<TOut> PutJsonAsync<TOut>(string requestUri, object content) => await ReadResponse<TOut>(HttpClient.PutAsync(requestUri, BuildContent(content)));

		public async Task<TOut> PutJsonAsync<TOut>(string requestUri, object content, CancellationToken cancellationToken) => await ReadResponse<TOut>(HttpClient.PutAsync(requestUri, BuildContent(content), cancellationToken));

		public async Task<TOut> PutJsonAsync<TOut>(Uri requestUri, object content, CancellationToken cancellationToken) => await ReadResponse<TOut>(HttpClient.PutAsync(requestUri, BuildContent(content), cancellationToken));

		public async Task<TOut> PutJsonAsync<TOut>(Uri requestUri, object content) => await ReadResponse<TOut>(HttpClient.PutAsync(requestUri, BuildContent(content)));

		public async Task PutJsonAsync(string requestUri, object content) => await HttpClient.PutAsync(requestUri, BuildContent(content));

		public async Task PutJsonAsync(string requestUri, object content, CancellationToken cancellationToken) => await HttpClient.PutAsync(requestUri, BuildContent(content), cancellationToken);

		public async Task PutJsonAsync(Uri requestUri, object content, CancellationToken cancellationToken) => await HttpClient.PutAsync(requestUri, BuildContent(content), cancellationToken);

		public async Task PutJsonAsync(Uri requestUri, object content) => await HttpClient.PutAsync(requestUri, BuildContent(content));

		private StringContent BuildContent(object toPost)
		{
			try
			{
				string json = JsonSerializer.Serialize(toPost, JsonSerializerSettings);

				return new StringContent(json, Encoding.UTF8, "application/json");
			}
			catch (Exception ex)
			{
				throw new SerializationFailureException(toPost, ex);
			}
		}

		private async Task<T> ReadResponse<T>(Task<HttpResponseMessage> responseTask)
		{
			HttpResponseMessage response = await responseTask;

			try
			{
				string content = await response.Content.ReadAsStringAsync();

				return JsonSerializer.Deserialize<T>(content, JsonSerializerSettings);
			}
			catch (Exception ex)
			{
				throw new DeserializationFailureException(response, ex);
			}
		}
	}
}