using Customer.SupportHub.Application.Services.Tokenization;
using Customer.SupportHub.Domain.DTOs.Responses;
using Customer.SupportHub.Domain.Services;

namespace Customer.SupportHub.Application.UseCases.SignOut;

public class SignOutUseCase(
	IRedisService redis,
	ITokenizationService tokenizationService
) : ISignOutUseCase
{
	public Task<ResponseDefault> ExecuteAsync(string token)
	{
		var accountId = tokenizationService.ValidateToken(token);

		redis.OutSessionAccountAsync(accountId.ToString());

		return Task.FromResult(new ResponseDefault(accountId.ToString(), MessageResponse.SIGN_OUT_CONFIRMADO));
	}
}