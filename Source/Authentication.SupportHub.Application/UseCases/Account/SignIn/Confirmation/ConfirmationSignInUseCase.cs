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

		return new ResponseToken
		{
			Token = tokenizationService.GenerateToken(accountId),
			RefreshToken = tokenizationService.GenerateRefreshToken(),
			ExpiryDate =
				DateTime.UtcNow.Add(TimeSpan.Parse(configuration["Jwt:Expiry"]!, CultureInfo.InvariantCulture))
		};
	}
}