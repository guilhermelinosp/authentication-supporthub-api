using System.Net;

namespace SupportHub.Auth.Domain.Shared.Returns;

public class Error(string statusCode, string message)
{
    #region Constructors

    public Error(HttpStatusCode httpStatusCode, string message) : this(
        ((int)httpStatusCode).ToString(), message)
    {
    }

    #endregion Constructors

    #region Properties

    public string StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;

    #endregion Properties

    #region Methods

    public static readonly Error None = new(string.Empty, string.Empty);

    #endregion Methods
}