using SupportHub.Authentication.Application.Services.Tokenization;
using SupportHub.Authentication.Domain.Cache;
using SupportHub.Authentication.Domain.DTOs.Responses;

namespace SupportHub.Authentication.Application.UseCases.Companies.SignOut;

public class SignOutUseCase(
	ISessionCache sessionCache,
	ITokenizationService tokenizationService
	) : ISignOutUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(string token)
	{
		var accountId = tokenizationService.ValidateToken(token);
		
		sessionCache.OutSessionAccountAsync(accountId.ToString());

		return new ResponseDefault(accountId.ToString(), MessagesResponse.SIGN_OUT_CONFIRMADO);
	}
}


