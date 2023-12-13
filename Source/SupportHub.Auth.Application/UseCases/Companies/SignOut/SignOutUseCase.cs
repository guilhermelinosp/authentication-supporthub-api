using SupportHub.Auth.Application.Services.Tokenization;
using SupportHub.Auth.Domain.Cache;
using SupportHub.Auth.Domain.DTOs.Responses;
using SupportHub.Auth.Domain.Exceptions;

namespace SupportHub.Auth.Application.UseCases.Companies.SignOut;

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


