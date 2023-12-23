using Company.SupportHub.Application.Services.Tokenization;
using Company.SupportHub.Domain.Cache;
using Company.SupportHub.Domain.DTOs.Responses;

namespace Company.SupportHub.Application.UseCases.Companies.SignOut;

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