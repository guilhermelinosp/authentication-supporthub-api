using System.Globalization;
using Company.SupportHub.Application.Services.Tokenization;
using Company.SupportHub.Domain.Cache;
using Company.SupportHub.Domain.DTOs.Responses;
using Company.SupportHub.Domain.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Company.SupportHub.Application.UseCases.Companies.SignIn.Confirmation;

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