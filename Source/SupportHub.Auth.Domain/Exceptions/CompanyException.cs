using System.Runtime.Serialization;

namespace SupportHub.Auth.Domain.Exceptions;

[Serializable]
public class CompanyException : BaseException
{
    public CompanyException(List<string>? errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }

    protected CompanyException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public List<string>? ErrorMessages { get; set; }
}