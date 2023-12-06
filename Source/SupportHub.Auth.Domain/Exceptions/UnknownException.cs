using System.Runtime.Serialization;

namespace SupportHub.Auth.Domain.Exceptions;

[Serializable]
public class UnknownException : BaseException
{
    public UnknownException(string message) : base(message)
    {
    }

    protected UnknownException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}