using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SupportHub.Auth.Domain.Dtos.Responses.Exceptions;
using SupportHub.Auth.Domain.Exceptions;

namespace SupportHub.Auth.API.Filters;

public class ValidationFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BaseException)
            HandleException(context);
        else
            HandleUnknownException(context);
    }

    private static void HandleException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ValidatorException:
                HandleValidationException(context);
                break;
            case CompanyException:
                HandleCompanyException(context);
                break;
            case TokenException:
                HandleTokenException(context);
                break;
            default:
                HandleUnknownException(context);
                break;
        }
    }

    private static void HandleValidationException(ExceptionContext context)
    {
        var exception = context.Exception as ValidatorException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ResponseException(exception!.ErrorMessages!));
    }

    private static void HandleCompanyException(ExceptionContext context)
    {
        var exception = context.Exception as CompanyException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ResponseException(exception!.ErrorMessages!));
    }

    private static void HandleUnknownException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        Console.WriteLine(context.Exception);
        context.Result =
            new ObjectResult(new ResponseException(new List<string> { MessagesException.ERRO_DESCONHECIDO }));
    }

    private static void HandleTokenException(ExceptionContext context)
    {
        var exception = context.Exception as TokenException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ResponseException(exception!.ErrorMessages!));
    }
}