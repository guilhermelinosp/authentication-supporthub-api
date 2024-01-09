using Authentication.SupportHub.Application.Services.Cryptography;
using Authentication.SupportHub.Application.UseCases.Validators;
using Authentication.SupportHub.Domain.DTOs.Requests;
using Authentication.SupportHub.Domain.DTOs.Responses;
using Authentication.SupportHub.Domain.Exceptions;
using Authentication.SupportHub.Domain.Messages;
using Authentication.SupportHub.Domain.Repositories;
using Authentication.SupportHub.Domain.Services;

namespace Authentication.SupportHub.Application.UseCases.Account.ResetPassword;

public class ResetPasswordUseCase(
	IAccountRepository repository,
	ICryptographyService cryptography,
	IRedisService redis)
	: IResetPasswordUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(RequestResetPassword request, string accountId, string code)
	{
		var validatorRequest = await new ValidatorResetPassword().ValidateAsync(request);
		if (!validatorRequest.IsValid)
			throw new DefaultException(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

		var validatorCode = redis.ValidateOneTimePassword(accountId, code);
		if (!validatorCode)
			throw new DefaultException([MessageException.CODIGO_INVALIDO]);

		var account = await repository.FindAccountByIdAsync(Guid.Parse(accountId));
		if (account is null)
			throw new DefaultException([MessageException.CONTA_NAO_ENCONTRADA]);

		if (request.Password != request.PasswordConfirmation)
			throw new DefaultException([MessageException.SENHA_NAO_CONFERE]);

		account.Password = cryptography.EncryptPassword(request.Password);

		await repository.UpdateAccountAsync(account);

		return new ResponseDefault
		{
			Message = MessageResponse.SENHA_RESETADA,
			AccountId = accountId
		};
	}
}