using System.Net;
using Customer.SupportHub.API.Controllers.Abstract;
using Customer.SupportHub.Application.UseCases.Employee.SignIn;
using Customer.SupportHub.Application.UseCases.Employee.SignOut;
using Customer.SupportHub.Domain.DTOs.Requests;
using Customer.SupportHub.Domain.DTOs.Responses;
using Customer.SupportHub.Domain.Exceptions;
using Customer.SupportHub.Domain.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Customer.SupportHub.API.Controllers;

[ApiController]
[Route("api/v1/customer/employee")]
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
		var token = Request.Headers.Authorization.ToString().Split(" ")[1];
		if (string.IsNullOrWhiteSpace(token)) throw new DefaultException([MessageException.TOKEN_NAO_INFORMADO]);
		var response = await signOut.ExecuteAsync(token);
		return new BaseActionResult<ResponseDefault>(HttpStatusCode.OK, response);
	}
}