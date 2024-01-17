using Authentication.SupportHub.Application.Services.Cryptography;
using Authentication.SupportHub.Application.UseCases.Validators;
using Authentication.SupportHub.Domain.DTOs.Messages;
using Authentication.SupportHub.Domain.DTOs.Requests;
using Authentication.SupportHub.Domain.DTOs.Responses;
using Authentication.SupportHub.Domain.Exceptions;
using Authentication.SupportHub.Domain.Repositories;
using Authentication.SupportHub.Domain.Services;

namespace Authentication.SupportHub.Application.UseCases.Account.SignIn;

public class SignInUseCase(
	IAccountRepository repository,
	ICryptographyService cryptographyService,
	ISendGridService sendGridService,
	ITwilioService twilioService,
	IRedisService redis)
	: ISignInUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(RequestSignInAccount request)
	{
		var validator = await new ValidatorSignInAccount().ValidateAsync(request);
		if (!validator.IsValid)
			throw new DefaultException(validator.Errors.Select(er => er.ErrorMessage).ToList());

		var account = await repository.FindAccountByIdentityAsync(request.Identity);
		if (account is null)
			throw new DefaultException([MessageException.IDENTITY_NAO_ENCONTRADO]);

		if (!cryptographyService.VerifyPassword(request.Password, account.Password))
			throw new DefaultException([MessageException.SENHA_INVALIDA]);

		if (account.IsDisabled)
			throw new DefaultException([MessageException.CONTA_DESATIVADA]);

		var session = redis.ValidateSessionStorageAsync(account.AccountId.ToString());
		if (session) throw new DefaultException([MessageException.SESSION_ATIVA]);

		var code = redis.GenerateOneTimePassword(account.AccountId.ToString());

		if (!account.IsVerified)
		{
			await sendGridService.SendSignUpAsync(account.Email, code);
			throw new DefaultException([MessageException.EMAIL_NAO_AUTENTICADO]);
		}

		if (account.Is2Fa)
			await twilioService.SendSignInAsync(account.Phone, code);
		else
			await sendGridService.SendSignInAsync(account.Email, code);

		return new ResponseDefault
		{
			Message = MessageResponse.CODIGO_ENVIADO,
			AccountId = account.AccountId.ToString()
		};
	}
}