using System.Reflection;
using Authentication.SupportHub.Application.Services.Cryptography;
using Authentication.SupportHub.Application.Services.Tokenization;
using Authentication.SupportHub.Application.UseCases.Account.ForgotPassword;
using Authentication.SupportHub.Application.UseCases.Account.ResetPassword;
using Authentication.SupportHub.Application.UseCases.Account.SignIn;
using Authentication.SupportHub.Application.UseCases.Account.SignIn.Confirmation;
using Authentication.SupportHub.Application.UseCases.Account.SignOut;
using Authentication.SupportHub.Application.UseCases.Account.SignUp;
using Authentication.SupportHub.Application.UseCases.Account.SignUp.Confirmation;
using Authentication.SupportHub.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.SupportHub.Application;

public static class ApplicationInjection
{
	public static void AddApplicationInjection(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddInfrastructureInjection(configuration);
		services.AddServices();
		services.AddUseCases();
	}

	private static void AddServices(this IServiceCollection services)
	{
		services.AddScoped<ICryptographyService, CryptographyService>();
		services.AddScoped<ITokenizationService, TokenizationService>();
	}

	private static void AddUseCases(this IServiceCollection services)
	{
		services.AddScoped<ISignInUseCase, SignInUseCase>();
		services.AddScoped<IConfirmationSignInUseCase, ConfirmationSignInUseCase>();
		services.AddScoped<ISignUpUseCase, SignUpUseCase>();
		services.AddScoped<IConfirmationSignUpUseCase, ConfirmationSignUpUseCase>();
		services.AddScoped<IForgotPasswordUseCase, ForgotPasswordUseCase>();
		services.AddScoped<IResetPasswordUseCase, ResetPasswordUseCase>();
		services.AddScoped<ISignOutUseCase, SignOutUseCase>();
	}
}