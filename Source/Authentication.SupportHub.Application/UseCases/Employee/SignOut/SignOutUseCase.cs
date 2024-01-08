using Authentication.SupportHub.Application.Services.Tokenization;
using Authentication.SupportHub.Domain.DTOs.Responses;
using Authentication.SupportHub.Domain.Messages;
using Authentication.SupportHub.Domain.Services;

namespace Authentication.SupportHub.Application.UseCases.Employee.SignOut;

public class SignOutUseCase(
	IRedisService redis,
	ITokenizationService tokenizationService
) : ISignOutUseCase
{
	public Task<ResponseDefault> ExecuteAsync(string token)
	{
		var accountId = tokenizationService.ValidateToken(token);

		redis.OutSessionStorageAsync(accountId.ToString());

		return Task.FromResult(new ResponseDefault(accountId.ToString(), MessageResponse.SIGN_OUT_CONFIRMADO));
	}
}