using System.Net;
using FluentValidation.Results;

namespace SupportHub.Auth.Domain.Shared.Returns;

public class BasicReturn(bool isSucess, Error error)
{
    #region Properties

    public bool IsSuccess { get; } = isSucess;
    public bool IsFailure => !IsSuccess;
    public Error Error { get; } = error;

    #endregion Properties

    #region Statics Methods

    #region Success

    public static BasicReturn Success() => new(true, Error.None);

    public static BasicReturn<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

    #endregion Success

    #region Failure

    public static BasicReturn Failure(Error error) => new(false, error);

    public static BasicReturn Failure(HttpStatusCode statusCode, List<ValidationFailure> errors) =>
        new(false, ReturnErrorMessage(((int)statusCode).ToString(), errors));

    public static BasicReturn<TValue> Failure<TValue>(Error error) => new(default, false, error);

    public static BasicReturn<TValue> Failure<TValue>(HttpStatusCode statusCode, List<ValidationFailure> errors) =>
        new(default, false, ReturnErrorMessage(((int)statusCode).ToString(), errors));

    #endregion Failure

    protected static BasicReturn<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.None);

    #endregion Statics Methods

    #region Private Methods

    private static Error ReturnErrorMessage(string statusCode, List<ValidationFailure> errors)
    {
        string errorMessage = string.Join(Environment.NewLine,
            errors.SelectMany(e => e.ErrorMessage!.Split('\n'))
                .Select(s => s.Trim()));

        return new Error(statusCode, errorMessage);
    }

    #endregion Private Methods
}