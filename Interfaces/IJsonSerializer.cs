namespace Loxifi.JsonClient.Interfaces
{
	public interface IJsonSerializer
	{
		T Deserialize<T>(string json, JsonSerializerSettings settings);

		string Serialize<T>(T objn, JsonSerializerSettings settings);
	}
}