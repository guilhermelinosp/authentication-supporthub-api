using Authentication.SupportHub.Domain.DTOs.Messages;
using Authentication.SupportHub.Domain.DTOs.Requests;
using Authentication.SupportHub.Domain.DTOs.Responses;
using Authentication.SupportHub.Domain.Exceptions;
using Authentication.SupportHub.Domain.Repositories;
using Authentication.SupportHub.Domain.Services;

namespace Authentication.SupportHub.Application.UseCases.Account.SignUp.Confirmation;

public class ConfirmationSignUpUseCase(
	IAccountRepository repository,
	IRedisService redis)
	: IConfirmationSignUpUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(string accountId, string code)
	{
		var validatorCode = redis.ValidateOneTimePassword(accountId, code);
		if (!validatorCode)
			throw new DefaultException([MessageException.CODIGO_INVALIDO]);

		var account = await repository.FindAccountByIdAsync(Guid.Parse(accountId));
		if (account is null)
			throw new DefaultException([MessageException.CONTA_NAO_ENCONTRADA]);

		account.IsVerified = true;

		await repository.UpdateAccountAsync(account);

		return new ResponseDefault
		{
			Message = MessageResponse.CODIGO_CONFIRMADO,
			AccountId = account.AccountId.ToString()
		};
	}
}