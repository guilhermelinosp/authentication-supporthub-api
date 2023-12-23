using System.Runtime.Serialization;

namespace Employee.SupportHub.Domain.Exceptions;

[Serializable]
public class ExceptionDefault : SystemException
{
	public ExceptionDefault(List<string>? errorMessages) : base(string.Empty)
	{
		ErrorMessages = errorMessages;
	}

	protected ExceptionDefault(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	public List<string>? ErrorMessages { get; set; }
}