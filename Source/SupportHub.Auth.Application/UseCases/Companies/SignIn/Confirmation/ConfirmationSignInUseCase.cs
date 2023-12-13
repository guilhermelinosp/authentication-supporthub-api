using System.Globalization;
using Microsoft.Extensions.Configuration;
using SupportHub.Auth.Application.Services.Tokenization;
using SupportHub.Auth.Application.UseCases.Companies.Validators;
using SupportHub.Auth.Domain.Cache;
using SupportHub.Auth.Domain.DTOs.Responses;
using SupportHub.Auth.Domain.Exceptions;
using SupportHub.Auth.Domain.Repositories;

namespace SupportHub.Auth.Application.UseCases.Companies.SignIn.Confirmation;

public class ConfirmationSignInUseCase(
    ITokenizationService tokenizationService,
    IConfiguration configuration,
    IOneTimePasswordCache oneTimePassword,
    ISessionCache session
) : IConfirmationSignInUseCase
{
    public  ResponseToken ExecuteAsync(string accountId, string code)
    {
        var validatorCode = oneTimePassword.ValidateOneTimePassword(accountId, code);
        if (!validatorCode)
            throw new DefaultException([MessagesException.CODIGO_INVALIDO]);
        
        session.SetSessionAccountAsync(accountId);
        
        return new ResponseToken
        {
            Token = tokenizationService.GenerateToken(accountId),
            RefreshToken = tokenizationService.GenerateRefreshToken(),
            ExpiryDate =
                DateTime.UtcNow.Add(TimeSpan.Parse(configuration["Jwt:Expiry"]!, CultureInfo.InvariantCulture))
        };
    }
}