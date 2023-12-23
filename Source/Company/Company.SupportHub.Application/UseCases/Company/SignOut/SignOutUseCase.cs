using Company.SupportHub.Application.Services.Tokenization;
using Company.SupportHub.Domain.DTOs.Responses;
using Company.SupportHub.Domain.Messages;
using Company.SupportHub.Domain.Services;

namespace Company.SupportHub.Application.UseCases.Company.SignOut;

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