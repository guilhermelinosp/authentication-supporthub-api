using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupportHub.Auth.Application.Abstract;
using SupportHub.Auth.Application.Services.Cryptography;
using SupportHub.Auth.Application.Services.Tokenization;
using SupportHub.Auth.Application.UseCases.Companies.ForgotPassword;
using SupportHub.Auth.Application.UseCases.Companies.ResetPassword;
using SupportHub.Auth.Application.UseCases.Companies.SignIn;
using SupportHub.Auth.Application.UseCases.Companies.SignIn.Confirmation;
using SupportHub.Auth.Application.UseCases.Companies.SignUp;
using SupportHub.Auth.Application.UseCases.Companies.SignUp.Confirmation;
using SupportHub.Auth.Infrastructure;

namespace SupportHub.Auth.Application;

public static class ApplicationInjection
{
    public static void AddApplicationInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddUseCases();
        services.AddInfrastructureInjection(configuration);
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEncryptService, EncryptService>();
        services.AddSingleton<ITokenService, TokenService>();
    }

    private static void AddUseCases(this IServiceCollection services)
    {

        services.AddScoped<ISignInUseCase, SignInUseCase>();
        services.AddScoped<ISignUpUseCase, SignUpUseCase>();
        services.AddScoped<IConfirmationSignUpUseCase, ConfirmationSignUpUseCase>();
        services.AddScoped<IConfirmationSignInUseCase, ConfirmationSignInUseCase>();
        services.AddScoped<IForgotPasswordUseCase, ForgotPasswordUseCase>();
        services.AddScoped<IResetPasswordUseCase, ResetPasswordUseCase>();
    }
}