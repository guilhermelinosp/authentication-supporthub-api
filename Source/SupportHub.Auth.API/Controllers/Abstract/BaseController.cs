using System.Net;
using Microsoft.AspNetCore.Mvc;
using SupportHub.Auth.Domain.Shared.Returns;

namespace SupportHub.Auth.API.Controllers.Abstract;

[ApiController]
[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseBase<string>))]
public abstract class BaseController : Controller
{
    protected ActionResult ResponseBase(HttpStatusCode statusCode, BasicReturn basicReturn, string responseMessage,
        HttpStatusCode statusCodeError = HttpStatusCode.NotFound)
    {
        if (basicReturn.IsFailure)
        {
            basicReturn.Error.StatusCode = ((int)statusCodeError).ToString();
            return StatusCode((int)statusCodeError, new ResponseBase<string>(basicReturn.Error));
        }

        return StatusCode((int)statusCode, new ResponseBase<string>(responseMessage));
    }
}