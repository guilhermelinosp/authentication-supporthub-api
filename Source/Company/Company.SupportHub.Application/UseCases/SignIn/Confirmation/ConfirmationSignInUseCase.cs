using System.Globalization;
using Company.SupportHub.Application.Services.Tokenization;
using Company.SupportHub.Domain.DTOs.Responses;
using Company.SupportHub.Domain.Exceptions;
using Company.SupportHub.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace Company.SupportHub.Application.UseCases.SignIn.Confirmation;

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
			throw new ExceptionDefault([MessageException.CODIGO_INVALIDO]);

		redis.SetSessionAccountAsync(accountId);

		return new ResponseToken
		{
			Token = tokenizationService.GenerateToken(accountId),
			RefreshToken = tokenizationService.GenerateRefreshToken(),
			ExpiryDate =
				DateTime.UtcNow.Add(TimeSpan.Parse(configuration["Jwt_Expiry"]!, CultureInfo.InvariantCulture))
		};
	}
}