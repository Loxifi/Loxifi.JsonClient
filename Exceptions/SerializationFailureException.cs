namespace Loxifi.Exceptions
{
	public class SerializationFailureException : Exception
	{
		public SerializationFailureException(object request, Exception innerException) : base("An exception has occurred serializing the request", innerException)
		{
			this.Request = request;
		}

		public object Request { get; private set; }
	}
}