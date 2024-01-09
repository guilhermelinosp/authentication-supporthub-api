using System.Globalization;
using Authentication.SupportHub.Application.Services.Tokenization;
using Authentication.SupportHub.Domain.DTOs.Responses;
using Authentication.SupportHub.Domain.Exceptions;
using Authentication.SupportHub.Domain.Messages;
using Authentication.SupportHub.Domain.Services;
using Microsoft.Extensions.Configuration;

namespace Authentication.SupportHub.Application.UseCases.Account.SignIn.Confirmation;

public class ConfirmationSignInUseCase(
	ITokenizationService tokenizationService,
	IConfiguration configuration,
	IRedisService redis
) : IConfirmationSignInUseCase
{
	public ResponseToken ExecuteAsync(string accountId, string code)
	{
		var validatorCode = redis.ValidateOneTimePassword(accountId, code);
		if (!validatorCode)
			throw new DefaultException([MessageException.CODIGO_INVALIDO]);

		redis.SetSessionStorageAsync(accountId);

		var token = tokenizationService.GenerateToken(accountId);

		var refreshToken = tokenizationService.GenerateRefreshToken();

		var expiryDate =
			DateTime.UtcNow.Add(TimeSpan.Parse(configuration["Jwt:Expiry"]!, CultureInfo.InvariantCulture));

		return new ResponseToken
		{
			Token = token,
			RefreshToken = refreshToken,
			ExpiryDate = expiryDate
		};
	}
}