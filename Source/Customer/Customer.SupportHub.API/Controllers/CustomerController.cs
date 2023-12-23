using System.Net;
using Customer.SupportHub.API.Controllers.Abstract;
using Customer.SupportHub.Application.UseCases.Customer.ForgotPassword;
using Customer.SupportHub.Application.UseCases.Customer.ForgotPassword.Confirmation;
using Customer.SupportHub.Application.UseCases.Customer.SignIn;
using Customer.SupportHub.Application.UseCases.Customer.SignIn.Confirmation;
using Customer.SupportHub.Application.UseCases.Employee.SignOut;
using Customer.SupportHub.Domain.DTOs.Requests;
using Customer.SupportHub.Domain.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Customer.SupportHub.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[ProducesResponseType<BaseActionResult<ResponseException>>(StatusCodes.Status400BadRequest)]
[ProducesResponseType<BaseActionResult<ResponseDefault>>(StatusCodes.Status200OK)]
public class CustomerController(
	IConfirmationSignInUseCase confirmationSignIn,
	ISignInUseCase signIn,
	ISignOutUseCase signOut,
	IForgotPasswordUseCase forgotPassword,
	IResetPasswordUseCase resetPassword) : Controller
{

	[HttpPost("signin")]
	public async Task<BaseActionResult<ResponseDefault>> SignInRequest([FromBody] RequestSignIn request)
	{
		var response = await signIn.ExecuteAsync(request);
		return new BaseActionResult<ResponseDefault>(HttpStatusCode.OK, response);
	}

	[HttpPost("signin/{accountId}/{otp}")]
	[ProducesResponseType<BaseActionResult<ResponseToken>>(StatusCodes.Status200OK)]
	public BaseActionResult<ResponseToken> SignInConfirmationRequest([FromRoute] string accountId,
		[FromRoute] string otp)
	{
		var response = confirmationSignIn.ExecuteAsync(accountId, otp);
		return new BaseActionResult<ResponseToken>(HttpStatusCode.OK, response);
	}


	[HttpGet("signout")]
	public async Task<BaseActionResult<ResponseDefault>> SignOutRequest()
	{
		var token = Request.Headers.Authorization.ToString().Split(" ")[1];
		var response = await signOut.ExecuteAsync(token);
		return new BaseActionResult<ResponseDefault>(HttpStatusCode.OK, response);
	}

	[HttpPost("forgot-password")]
	public async Task<BaseActionResult<ResponseDefault>> ForgotPasswordRequest([FromBody] RequestForgotPassword request)
	{
		var response = await forgotPassword.ExecuteAsync(request);
		return new BaseActionResult<ResponseDefault>(HttpStatusCode.OK, response);
	}

	[HttpPost("forgot-password/{accountId}/{otp}")]
	public async Task<BaseActionResult<ResponseDefault>> ResetPasswordRequest([FromBody] RequestResetPassword request,
		[FromRoute] string accountId, [FromRoute] string otp)
	{
		var responde = await resetPassword.ExecuteAsync(request, accountId, otp);
		return new BaseActionResult<ResponseDefault>(HttpStatusCode.OK, responde);
	}
}