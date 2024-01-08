using Authentication.SupportHub.Application.Services.Cryptography;
using Authentication.SupportHub.Application.UseCases.Validators;
using Authentication.SupportHub.Domain.DTOs.Requests;
using Authentication.SupportHub.Domain.DTOs.Responses;
using Authentication.SupportHub.Domain.Exceptions;
using Authentication.SupportHub.Domain.Messages;
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
	public async Task<ResponseDefault> ExecuteAsync(RequestSignInCompany request)
	{
		var validator = await new ValidatorSignInAccount().ValidateAsync(request);
		if (!validator.IsValid)
			throw new DefaultException(validator.Errors.Select(er => er.ErrorMessage).ToList());

		var account = await repository.FindAccountByCnpjAsync(request.Cnpj);
		if (account is null)
			throw new DefaultException([MessageException.EMAIL_NAO_ENCONTRADO]);

		if (!cryptographyService.VerifyPassword(request.Password, account.Password))
			throw new DefaultException([MessageException.SENHA_INVALIDA]);

		var code = redis.GenerateOneTimePassword(account.AccountId.ToString());

		if (!account.IsVerified)
		{
			await sendGridService.SendSignUpAsync(account.Email, code);
			throw new DefaultException([MessageException.EMAIL_NAO_AUTENTICADO]);
		}

		await repository.UpdateAccountAsync(account);

		if (account.Is2FaEnabled)
			await twilioService.SendSignInAsync(account.Phone, code);
		else
			await sendGridService.SendSignInAsync(account.Email, code);

		return new ResponseDefault(account.AccountId.ToString(), MessageResponse.CODIGO_ENVIADO);
	}
}