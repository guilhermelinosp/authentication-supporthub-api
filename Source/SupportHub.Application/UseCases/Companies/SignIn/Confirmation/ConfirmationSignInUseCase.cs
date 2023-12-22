using System.Globalization;
using Microsoft.Extensions.Configuration;
using SupportHub.Authentication.Application.Services.Tokenization;
using SupportHub.Domain.Cache;
using SupportHub.Domain.DTOs.Responses;
using SupportHub.Domain.Exceptions;

namespace SupportHub.Authentication.Application.UseCases.Companies.SignIn.Confirmation;

public class ConfirmationSignInUseCase(
	ITokenizationService tokenizationService,
	IConfiguration configuration,
	IOneTimePasswordCache oneTimePassword,
	ISessionCache session
) : IConfirmationSignInUseCase
{
	public ResponseToken ExecuteAsync(string accountId, string code)
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
				DateTime.UtcNow.Add(TimeSpan.Parse(configuration["Jwt_Expiry"]!, CultureInfo.InvariantCulture))
		};
	}
}