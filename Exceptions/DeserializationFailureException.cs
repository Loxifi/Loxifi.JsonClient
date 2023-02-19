namespace Loxifi.Exceptions
{
	public class DeserializationFailureException : Exception
	{
		public DeserializationFailureException(HttpResponseMessage response, Exception innerException) : base("An exception has occurred deserializing the response", innerException)
		{
			Response = response;
		}

		public HttpResponseMessage Response { get; private set; }
	}
}