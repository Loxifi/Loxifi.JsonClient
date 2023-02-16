using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Loxifi.Exceptions
{
	public class DeserializationFailureException : Exception
	{
		public HttpResponseMessage Response { get; private set; }
		
		public DeserializationFailureException(HttpResponseMessage response, Exception innerException) : base("An exception has occurred deserializing the response", innerException)
		{
			Response = response;
		}
	}
}
