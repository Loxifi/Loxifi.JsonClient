using Loxifi;
using Loxifi.Interfaces;
using Loxifi.Serializers;

namespace Loxifi.Settings
{
    public class JsonClientSettings
    {
        public HttpClient HttpClient { get; set; } = new HttpClient();

        public IJsonSerializer JsonSerializer { get; set; } = new SystemTextJsonSerializer();

        public JsonSerializerSettings JsonSerializerSettings { get; set; } = new JsonSerializerSettings();
    }
}