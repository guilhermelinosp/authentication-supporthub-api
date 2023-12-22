using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SupportHub.Domain.DTOs.Responses;
using SupportHub.Domain.Exceptions;

namespace SupportHub.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
	public void OnException(ExceptionContext context)
	{
		if (context.Exception is DefaultException exception)
			context.Result = new ObjectResult(new { data = new ResponseException(exception.ErrorMessages!.ToList()) })
			{
				StatusCode = (int)HttpStatusCode.BadRequest
			};
		else
			context.Result = new ObjectResult(new
				{ data = new ResponseException([MessagesException.ERRO_DESCONHECIDO]) })
			{
				StatusCode = (int)HttpStatusCode.InternalServerError
			};
	}
}