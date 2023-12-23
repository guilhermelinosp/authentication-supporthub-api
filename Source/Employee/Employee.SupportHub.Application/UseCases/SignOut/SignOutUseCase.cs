using Employee.SupportHub.Application.Services.Tokenization;
using Employee.SupportHub.Domain.DTOs.Responses;
using Employee.SupportHub.Domain.Messages;
using Employee.SupportHub.Domain.Services;

namespace Employee.SupportHub.Application.UseCases.SignOut;

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