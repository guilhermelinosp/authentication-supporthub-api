using System.Runtime.Serialization;

namespace SupportHub.Auth.Domain.Exceptions;

[Serializable]
public class ValidatorException : BaseException
{
    public ValidatorException(List<string>? errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }

    protected ValidatorException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public List<string>? ErrorMessages { get; set; }
}