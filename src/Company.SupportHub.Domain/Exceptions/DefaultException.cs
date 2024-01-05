using System.Runtime.Serialization;

namespace Company.SupportHub.Domain.Exceptions;

[Serializable]
public class DefaultException(List<string>? errorMessages) : SystemException(string.Empty)
{
	public List<string>? ErrorMessages { get; set; } = errorMessages;
}