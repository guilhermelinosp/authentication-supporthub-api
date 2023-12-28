using System.Net;
using Company.SupportHub.API.Controllers.Abstract;
using Company.SupportHub.Application.UseCases.Employee.SignIn;
using Company.SupportHub.Application.UseCases.Employee.SignOut;
using Company.SupportHub.Domain.DTOs.Requests;
using Company.SupportHub.Domain.DTOs.Responses;
using Company.SupportHub.Domain.Exceptions;
using Company.SupportHub.Domain.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Company.SupportHub.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[ProducesResponseType<BaseActionResult<ResponseException>>(StatusCodes.Status400BadRequest)]
[ProducesResponseType<BaseActionResult<ResponseDefault>>(StatusCodes.Status200OK)]
public class EmployeeController(
	ISignInUseCase signIn,
	ISignOutUseCase signOut) : Controller
{
	[HttpPost("signin")]
	public async Task<BaseActionResult<ResponseToken>> SignInRequest([FromBody] RequestSignIn request)
	{
		var response = await signIn.ExecuteAsync(request);
		return new BaseActionResult<ResponseToken>(HttpStatusCode.OK, response);
	}

	[HttpGet("signout")]
	public async Task<BaseActionResult<ResponseDefault>> SignOutRequest()
	{
		var token = HttpContext!.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
		if (string.IsNullOrWhiteSpace(token))
		{
			throw new ExceptionDefault([MessageException.TOKEN_NAO_INFORMADO]);
		}
		
		var response = await signOut.ExecuteAsync(token);
		return new BaseActionResult<ResponseDefault>(HttpStatusCode.OK, response);
	}
}