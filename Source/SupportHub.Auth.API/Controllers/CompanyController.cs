using Microsoft.AspNetCore.Mvc;
using SupportHub.Auth.Application.UseCases.Companies.ForgotPassword;
using SupportHub.Auth.Application.UseCases.Companies.ResetPassword;
using SupportHub.Auth.Application.UseCases.Companies.SignIn;
using SupportHub.Auth.Application.UseCases.Companies.SignIn.Confirmation;
using SupportHub.Auth.Application.UseCases.Companies.SignUp;
using SupportHub.Auth.Application.UseCases.Companies.SignUp.Confirmation;
using SupportHub.Auth.Domain.Dtos.Requests.Companies;
using SupportHub.Auth.Domain.Dtos.Responses.Companies;
using SupportHub.Auth.Domain.Dtos.Responses.Exceptions;

namespace SupportHub.Auth.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType<ResponseException>(StatusCodes.Status400BadRequest)]
public class CompanyController(
    ISignUpUseCase signUp,
    IConfirmationSignUpUseCase confirmationSignUp,
    IConfirmationSignInUseCase confirmationSignIn,
    ISignInUseCase signIn,
    IForgotPasswordUseCase forgotPassword,
    IResetPasswordUseCase resetPassword) : Controller
{
    
    [HttpPost("signup")]
    public async Task<IActionResult> SignUpRequest([FromBody] RequestSignUp request)
    {
        await signUp.ExecuteAsync(request);
        return Ok(new { message = "sending the code to your email." });
    }
    
    [HttpPost("signup/{otp}")]
    public async Task<IActionResult> SignUpConfirmationRequest([FromRoute] string otp)
    {
        await confirmationSignUp.ExecuteAsync(otp);
        return Ok(new { message = "confirmed successfully." });
    }
    
    [HttpPost("signin")]
    public async Task<IActionResult> SignInEmailRequest([FromBody] RequestSignInEmail request)
    {
        await signIn.ExecuteAsync(request);
        return Ok(new { message = "sending the code to your email or phone." });
    }
    
    [ProducesResponseType(typeof(ResponseSignIn), StatusCodes.Status200OK)]
    [HttpPost("signin/{otp}")]
    public async Task<IActionResult> SignInEmailConfirmationRequest([FromRoute] string otp)
    {
        var response = await confirmationSignIn.ExecuteAsync(otp);
        return Ok(response);
    }
    
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPasswordRequest([FromBody] RequestForgotPassword request)
    {
        await forgotPassword.ExecuteAsync(request);
        return Ok(new { message = "sending the code to your email or phone." });
    }
    
    [HttpPost("reset-password/{otp}")]
    public async Task<IActionResult> ResetPasswordRequest([FromBody] RequestResetPassword request, [FromRoute] string otp)
    {
        await resetPassword.ExecuteAsync(request, otp);
        return Ok(new { message = "confirmed successfully." });
    }
}