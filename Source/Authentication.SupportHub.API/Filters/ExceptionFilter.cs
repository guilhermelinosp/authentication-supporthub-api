using System.Net;
using Authentication.SupportHub.Domain.DTOs.Responses;
using Authentication.SupportHub.Domain.Exceptions;
using Authentication.SupportHub.Domain.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Authentication.SupportHub.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
	public void OnException(ExceptionContext context)
	{
		Console.WriteLine(context.Exception);

		if (context.Exception is DefaultException exception)
			context.Result = new ObjectResult(new { data = new ResponseException(exception.ErrorMessages!.ToList()) })
			{
				StatusCode = (int)HttpStatusCode.BadRequest
			};
		else
			context.Result =
				new ObjectResult(new { data = new ResponseException([MessageException.ERRO_DESCONHECIDO]) })
				{
					StatusCode = (int)HttpStatusCode.InternalServerError
				};
	}
}