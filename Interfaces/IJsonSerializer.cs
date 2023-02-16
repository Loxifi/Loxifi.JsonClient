using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loxifi.JsonClient.Interfaces
{
	public interface IJsonSerializer
	{
		T Deserialize<T>(string json, JsonSerializerSettings settings);	
		string Serialize<T>(T objn, JsonSerializerSettings settings);
	}
}
