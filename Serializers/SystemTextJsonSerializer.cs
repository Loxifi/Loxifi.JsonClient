using Loxifi.JsonClient.Interfaces;
using System;
using System.Text.Json;

namespace Loxifi.JsonClient.Serializers
{
	public class SystemTextJsonSerializer : IJsonSerializer
	{
		private JsonSerializerOptions ConvertSettings(JsonSerializerSettings settings) => new();
		public T Deserialize<T>(string json, JsonSerializerSettings settings = null) => throw new NotImplementedException();
		public string Serialize<T>(T objn, JsonSerializerSettings settings = null) => throw new NotImplementedException();
	}
}
