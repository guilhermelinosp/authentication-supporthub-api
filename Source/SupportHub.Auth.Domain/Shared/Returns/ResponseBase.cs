namespace SupportHub.Auth.Domain.Shared.Returns;

public class ResponseBase<T>(T? data, Error error)
{
    #region Properties

    public T? Data { get; set; } = data;
    public Error Error { get; set; } = error;

    #endregion Properties

    #region Constructors

    public ResponseBase(T? data) : this(data, Error.None)
    {
        Data = data;
    }

    public ResponseBase(Error error) : this(default, error)
    {
        Error = error;
    }

    #endregion Constructors
}