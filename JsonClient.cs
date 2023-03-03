using Loxifi.Exceptions;
using Loxifi.Interfaces;
using Loxifi.Settings;
using System.Text;

namespace Loxifi
{
	public class JsonClient
	{
		public JsonClient(JsonClientSettings jsonClientSettings)
		{
			this.HttpClient = jsonClientSettings.HttpClient;
			this.JsonSerializer = jsonClientSettings.JsonSerializer;
			this.JsonSerializerSettings = jsonClientSettings.JsonSerializerSettings;
		}

		public JsonClient() : this(new JsonClientSettings())
		{
		}

		public HttpClient HttpClient { get; private set; }

		public IJsonSerializer JsonSerializer { get; private set; }

		public JsonSerializerSettings JsonSerializerSettings { get; private set; }

		public async Task<TOut> GetJsonAsync<TOut>(string requestUri, CancellationToken cancellationToken) => await this.ReadResponse<TOut>(this.HttpClient.GetAsync(requestUri, cancellationToken));

		public async Task<TOut> GetJsonAsync<TOut>(Uri requestUri) => await this.ReadResponse<TOut>(this.HttpClient.GetAsync(requestUri));

		public async Task<TOut> GetJsonAsync<TOut>(Uri requestUri, HttpCompletionOption completionOption) => await this.ReadResponse<TOut>(this.HttpClient.GetAsync(requestUri, completionOption));

		public async Task<TOut> GetJsonAsync<TOut>(string requestUri, HttpCompletionOption completionOption) => await this.ReadResponse<TOut>(this.HttpClient.GetAsync(requestUri, completionOption));

		public async Task<TOut> GetJsonAsync<TOut>(Uri requestUri, CancellationToken cancellationToken) => await this.ReadResponse<TOut>(this.HttpClient.GetAsync(requestUri, cancellationToken));

		public async Task<TOut> GetJsonAsync<TOut>(string requestUri) => await this.ReadResponse<TOut>(this.HttpClient.GetAsync(requestUri));

		public async Task<TOut> PatchJsonAsync<TOut>(Uri requestUri, object content) => await this.ReadResponse<TOut>(this.HttpClient.PatchAsync(requestUri, this.BuildContent(content)));

		public async Task<TOut> PatchJsonAsync<TOut>(string requestUri, object content) => await this.ReadResponse<TOut>(this.HttpClient.PatchAsync(requestUri, this.BuildContent(content)));

		public async Task<TOut> PatchJsonAsync<TOut>(string requestUri, object content, CancellationToken cancellationToken) => await this.ReadResponse<TOut>(this.HttpClient.PatchAsync(requestUri, this.BuildContent(content), cancellationToken));

		public async Task<TOut> PatchJsonAsync<TOut>(Uri requestUri, object content, CancellationToken cancellationToken) => await this.ReadResponse<TOut>(this.HttpClient.PatchAsync(requestUri, this.BuildContent(content), cancellationToken));

		public async Task PatchJsonAsync(Uri requestUri, object content) => await this.HttpClient.PatchAsync(requestUri, this.BuildContent(content));

		public async Task PatchJsonAsync(string requestUri, object content) => await this.HttpClient.PatchAsync(requestUri, this.BuildContent(content));

		public async Task PatchJsonAsync(string requestUri, object content, CancellationToken cancellationToken) => await this.HttpClient.PatchAsync(requestUri, this.BuildContent(content), cancellationToken);

		public async Task PatchJsonAsync(Uri requestUri, object content, CancellationToken cancellationToken) => await this.HttpClient.PatchAsync(requestUri, this.BuildContent(content), cancellationToken);

		public async Task<TOut> PostJsonAsync<TOut>(Uri requestUri, object content) => await this.ReadResponse<TOut>(this.HttpClient.PostAsync(requestUri, this.BuildContent(content)));

		public async Task<TOut> PostJsonAsync<TOut>(Uri requestUri, object content, CancellationToken cancellationToken) => await this.ReadResponse<TOut>(this.HttpClient.PostAsync(requestUri, this.BuildContent(content), cancellationToken));

		public async Task<TOut> PostJsonAsync<TOut>(string requestUri, object content) => await this.ReadResponse<TOut>(this.HttpClient.PostAsync(requestUri, this.BuildContent(content)));

		public async Task<TOut> PostJsonAsync<TOut>(string requestUri, object content, CancellationToken cancellationToken) => await this.ReadResponse<TOut>(this.HttpClient.PostAsync(requestUri, this.BuildContent(content), cancellationToken));

		public async Task PostJsonAsync(Uri requestUri, object content) => await this.HttpClient.PostAsync(requestUri, this.BuildContent(content));

		public async Task PostJsonAsync(Uri requestUri, object content, CancellationToken cancellationToken) => await this.HttpClient.PostAsync(requestUri, this.BuildContent(content), cancellationToken);

		public async Task PostJsonAsync(string requestUri, object content) => await this.HttpClient.PostAsync(requestUri, this.BuildContent(content));

		public async Task PostJsonAsync(string requestUri, object content, CancellationToken cancellationToken) => await this.HttpClient.PostAsync(requestUri, this.BuildContent(content), cancellationToken);

		public async Task<TOut> PutJsonAsync<TOut>(string requestUri, object content) => await this.ReadResponse<TOut>(this.HttpClient.PutAsync(requestUri, this.BuildContent(content)));

		public async Task<TOut> PutJsonAsync<TOut>(string requestUri, object content, CancellationToken cancellationToken) => await this.ReadResponse<TOut>(this.HttpClient.PutAsync(requestUri, this.BuildContent(content), cancellationToken));

		public async Task<TOut> PutJsonAsync<TOut>(Uri requestUri, object content, CancellationToken cancellationToken) => await this.ReadResponse<TOut>(this.HttpClient.PutAsync(requestUri, this.BuildContent(content), cancellationToken));

		public async Task<TOut> PutJsonAsync<TOut>(Uri requestUri, object content) => await this.ReadResponse<TOut>(this.HttpClient.PutAsync(requestUri, this.BuildContent(content)));

		public async Task PutJsonAsync(string requestUri, object content) => await this.HttpClient.PutAsync(requestUri, this.BuildContent(content));

		public async Task PutJsonAsync(string requestUri, object content, CancellationToken cancellationToken) => await this.HttpClient.PutAsync(requestUri, this.BuildContent(content), cancellationToken);

		public async Task PutJsonAsync(Uri requestUri, object content, CancellationToken cancellationToken) => await this.HttpClient.PutAsync(requestUri, this.BuildContent(content), cancellationToken);

		public async Task PutJsonAsync(Uri requestUri, object content) => await this.HttpClient.PutAsync(requestUri, this.BuildContent(content));

		public async Task<TOut> DeleteJsonAsync<TOut>(string requestUri) => await ReadResponse<TOut>(HttpClient.DeleteAsync(requestUri));

		public async Task<TOut> DeleteJsonAsync<TOut>(string requestUri, CancellationToken cancellationToken) => await ReadResponse<TOut>(HttpClient.DeleteAsync(requestUri, cancellationToken));

		public async Task<TOut> DeleteJsonAsync<TOut>(Uri requestUri, CancellationToken cancellationToken) => await ReadResponse<TOut>(HttpClient.DeleteAsync(requestUri, cancellationToken));

		public async Task<TOut> DeleteJsonAsync<TOut>(Uri requestUri) => await ReadResponse<TOut>(HttpClient.DeleteAsync(requestUri));

		private StringContent BuildContent(object toPost)
		{
			try
			{
				string json = this.JsonSerializer.Serialize(toPost, this.JsonSerializerSettings);

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

				return this.JsonSerializer.Deserialize<T>(content, this.JsonSerializerSettings);
			}
			catch (Exception ex)
			{
				throw new DeserializationFailureException(response, ex);
			}
		}
	}
}