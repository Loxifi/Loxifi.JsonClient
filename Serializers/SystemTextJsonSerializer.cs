using Loxifi.JsonClient.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Loxifi.JsonClient.Serializers
{
	public class SystemTextJsonSerializer : IJsonSerializer
	{
		private JsonSerializerOptions BuildOptions(JsonSerializerSettings settings) => new();
		public T Deserialize<T>(string json, JsonSerializerSettings settings = null) => JsonSerializer.Deserialize<T>(json, BuildOptions(settings));

		public string Serialize<T>(T objn, JsonSerializerSettings settings = null) => JsonSerializer.Serialize(objn, BuildOptions(settings));

		private JsonSerializerOptions ConvertSettings(JsonSerializerSettings settings) => new();
	}
}