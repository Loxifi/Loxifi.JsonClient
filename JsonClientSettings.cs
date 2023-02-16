using Loxifi.JsonClient.Interfaces;
using Loxifi.JsonClient.Serializers;
using System.Net.Http;

namespace Loxifi.JsonClient
{
	public class JsonClientSettings
	{
		public HttpClient HttpClient { get; set; } = new HttpClient();
		public IJsonSerializer JsonSerializer { get; set; } = new SystemTextJsonSerializer();
		public JsonSerializerSettings JsonSerializerSettings { get; set; } = new JsonSerializerSettings();

	}
}
