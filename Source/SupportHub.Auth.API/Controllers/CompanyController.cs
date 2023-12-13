using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportHub.Auth.API.Controllers.Abstract;
using SupportHub.Auth.Application.UseCases.Companies.ForgotPassword;
using SupportHub.Auth.Application.UseCases.Companies.ForgotPassword.Confirmation;
using SupportHub.Auth.Application.UseCases.Companies.SignIn;
using SupportHub.Auth.Application.UseCases.Companies.SignIn.Confirmation;
using SupportHub.Auth.Application.UseCases.Companies.SignOut;
using SupportHub.Auth.Application.UseCases.Companies.SignUp;
using SupportHub.Auth.Application.UseCases.Companies.SignUp.Confirmation;
using SupportHub.Auth.Domain.DTOs.Requests.Companies;
using SupportHub.Auth.Domain.DTOs.Responses;

namespace SupportHub.Auth.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[ProducesResponseType<BaseActionResult<ResponseException>>(StatusCodes.Status400BadRequest)]
[ProducesResponseType<BaseActionResult<ResponseDefault>>(StatusCodes.Status200OK)]
public class CompanyController(
    ISignUpUseCase signUp,
    IConfirmationSignUpUseCase confirmationSignUp,
    IConfirmationSignInUseCase confirmationSignIn,
    ISignInUseCase signIn,
    ISignOutUseCase signOut,
    IForgotPasswordUseCase forgotPassword,
    IResetPasswordUseCase resetPassword) : Controller
{
    [HttpPost("signup")]
    public async Task<BaseActionResult<ResponseDefault>> SignUpRequest([FromBody] RequestSignUp request)
    {
        var response = await signUp.ExecuteAsync(request);
        return new BaseActionResult<ResponseDefault>(HttpStatusCode.OK,response);
    }
    
    [HttpPost("signup/{accountId}/{otp}")]
    public async Task<BaseActionResult<ResponseDefault>> SignUpConfirmationRequest([FromRoute] string accountId,[FromRoute] string otp)
    {
        var responde = await confirmationSignUp.ExecuteAsync(accountId, otp);
        return new BaseActionResult<ResponseDefault>(HttpStatusCode.OK, responde);
    }

    [HttpPost("signin")]
    public async Task<BaseActionResult<ResponseDefault>> SignInRequest([FromBody] RequestSignIn request)
    {
        var response = await signIn.ExecuteAsync(request);
        return new BaseActionResult<ResponseDefault>(HttpStatusCode.OK, response);
    }
    
    [HttpPost("signin/{accountId}/{otp}")]
    [ProducesResponseType<BaseActionResult<ResponseToken>>(StatusCodes.Status200OK)]
    public  BaseActionResult<ResponseToken> SignInConfirmationRequest([FromRoute] string accountId,[FromRoute] string otp)
    {
        var response = confirmationSignIn.ExecuteAsync(accountId,otp);
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
       return new BaseActionResult<ResponseDefault>(HttpStatusCode.OK,response);
   }
   
   [HttpPost("forgot-password/{accountId}/{otp}")]
   public async Task<BaseActionResult<ResponseDefault>> ResetPasswordRequest([FromBody] RequestResetPassword request,[FromRoute] string accountId, [FromRoute] string otp)
   {
       var responde = await resetPassword.ExecuteAsync(request,accountId, otp);
       return new BaseActionResult<ResponseDefault>(HttpStatusCode.OK,responde);
   }
}

