using System.Net;
using Microsoft.AspNetCore.Mvc;
using SupportHub.Authentication.Domain.Shared.Returns;

namespace SupportHub.Authentication.API.Controllers.Abstract;

[ApiController]
public abstract class BaseController : Controller
{
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseBase<string>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	protected ActionResult ResponseBase(HttpStatusCode statusCode, BasicReturn basicReturn, string responseMessage,
		HttpStatusCode statusCodeError = HttpStatusCode.NotFound)
	{
		if (!basicReturn.IsFailure) return StatusCode((int)statusCode, new ResponseBase<string?>(responseMessage));

		basicReturn.Error.StatusCode = ((int)statusCodeError).ToString();
		return StatusCode((int)statusCodeError, new ResponseBase<string>(basicReturn.Error));
	}

	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseBase<string>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	protected ActionResult ResponseBase<T>(HttpStatusCode statusCode, BasicReturn<T> basicReturn,
		HttpStatusCode statusCodeError = HttpStatusCode.NotFound)
	{
		if (!basicReturn.IsFailure) return StatusCode((int)statusCode, new ResponseBase<T?>(basicReturn.Value));

		basicReturn.Error.StatusCode = ((int)statusCodeError).ToString();
		return StatusCode((int)statusCodeError, new ResponseBase<string>(basicReturn.Error));
	}
}