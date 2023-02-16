using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Loxifi.Exceptions
{
	public class SerializationFailureException : Exception
	{
		public object Request { get; private set; }
		
		public SerializationFailureException(object request, Exception innerException) : base("An exception has occurred serializing the request", innerException)
		{
			Request = request;
		}
	}
}
