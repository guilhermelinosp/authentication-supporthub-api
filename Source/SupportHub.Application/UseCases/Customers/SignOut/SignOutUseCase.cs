using SupportHub.Application.Services.Tokenization;
using SupportHub.Domain.Cache;
using SupportHub.Domain.DTOs.Responses;

namespace SupportHub.Application.UseCases.Customers.SignOut;

public class SignOutUseCase(
	ISessionCache sessionCache,
	ITokenizationService tokenizationService
) : ISignOutUseCase
{
	public Task<ResponseDefault> ExecuteAsync(string token)
	{
		var accountId = tokenizationService.ValidateToken(token);

		sessionCache.OutSessionAccountAsync(accountId.ToString());

		return Task.FromResult(new ResponseDefault(accountId.ToString(), MessagesResponse.SIGN_OUT_CONFIRMADO));
	}
}