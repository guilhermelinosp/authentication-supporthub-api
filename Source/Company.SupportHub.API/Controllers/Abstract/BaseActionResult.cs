using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Company.SupportHub.API.Controllers.Abstract;

public class BaseActionResult<T>(HttpStatusCode statusCode, T? data) : IActionResult
{
	public T? Data => data;

	public Task ExecuteResultAsync(ActionContext context)
	{
		var objectResult = new ObjectResult(new { data = Data })
		{
			StatusCode = (int)statusCode
		};

		return objectResult.ExecuteResultAsync(context);
	}
}