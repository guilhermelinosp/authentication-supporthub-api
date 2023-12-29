using System.Runtime.Serialization;

namespace Company.SupportHub.Domain.Exceptions;

[Serializable]
public class DefaultException : SystemException
{
	public List<string>? ErrorMessages { get; set; }

	public DefaultException(List<string>? errorMessages) : base(string.Empty)
	{
		ErrorMessages = errorMessages;
	}

	protected DefaultException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}