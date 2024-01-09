using Authentication.SupportHub.Application.Services.Cryptography;
using Authentication.SupportHub.Application.UseCases.Validators;
using Authentication.SupportHub.Domain.APIs;
using Authentication.SupportHub.Domain.DTOs.Requests;
using Authentication.SupportHub.Domain.DTOs.Responses;
using Authentication.SupportHub.Domain.Exceptions;
using Authentication.SupportHub.Domain.Messages;
using Authentication.SupportHub.Domain.Repositories;
using Authentication.SupportHub.Domain.Services;

namespace Authentication.SupportHub.Application.UseCases.Account.SignUp;

public class SignUpUseCase(
	IAccountRepository repository,
	ICryptographyService cryptography,
	ISendGridService sendGridService,
	IBrazilApi brazilApi,
	IRedisService redis)
	: ISignUpUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(RequestSignUpAccount request)
	{
		var validatorRequest = await new ValidatorSignUpAccount().ValidateAsync(request);
		if (!validatorRequest.IsValid)
			throw new DefaultException(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

		var checkCnpj = await brazilApi.Consultation(request.Cnpj);
		if (!checkCnpj)
			throw new DefaultException([MessageException.IDENTITY_INVALIDO]);

		var validateCnpj = await repository.FindAccountByIdentityAsync(request.Cnpj);
		if (validateCnpj is not null)
			throw new DefaultException([MessageException.IDENTITY_JA_REGISTRADO]);

		var validateEmail = await repository.FindAccountByEmailAsync(request.Email);
		if (validateEmail is not null)
			throw new DefaultException([MessageException.EMAIL_JA_REGISTRADO]);

		if (request.Password != request.PasswordConfirmation)
			throw new DefaultException([MessageException.SENHA_NAO_CONFERE]);

		var account = new Domain.Entities.Account
		{
			Identity = request.Cnpj,
			Email = request.Email,
			Password = cryptography.EncryptPassword(request.Password)
		};

		await repository.CreateAccountAsync(account);

		var code = redis.GenerateOneTimePassword(account.AccountId.ToString());

		await sendGridService.SendSignUpAsync(request.Email, code);

		return new ResponseDefault
		{
			Message = account.AccountId.ToString(),
			AccountId = MessageResponse.CODIGO_ENVIADO_SIGN_UP
		};
	}
}