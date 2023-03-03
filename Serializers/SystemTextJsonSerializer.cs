using Loxifi.Interfaces;
using System.Text.Json;

namespace Loxifi.Serializers
{
	public class SystemTextJsonSerializer : IJsonSerializer
	{
		private JsonSerializerOptions BuildOptions(JsonSerializerSettings settings)
		{
			JsonSerializerOptions options = new()
			{
				DefaultIgnoreCondition = settings.DefaultValueHandling switch
				{
					Settings.DefaultValueHandling.IgnoreDefault => System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault,
					Settings.DefaultValueHandling.IgnoreNull => System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
					_ => throw new NotImplementedException(),
				}
			};

			return options;
		}
		public T Deserialize<T>(string json, JsonSerializerSettings settings = null) => JsonSerializer.Deserialize<T>(json, this.BuildOptions(settings));

		public string Serialize<T>(T objn, JsonSerializerSettings settings = null) => JsonSerializer.Serialize(objn, this.BuildOptions(settings));
	}
}