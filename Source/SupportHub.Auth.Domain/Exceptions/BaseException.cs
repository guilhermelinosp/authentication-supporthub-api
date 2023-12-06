using System.Runtime.Serialization;

namespace SupportHub.Auth.Domain.Exceptions;

[Serializable]
public class BaseException : SystemException
{
    public BaseException(string message) : base(message)
    {
    }

    protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}