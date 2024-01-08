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
	public async Task<ResponseDefault> ExecuteAsync(RequestSignUp request)
	{
		var validatorRequest = await new ValidatorSignUpAccount().ValidateAsync(request);
		if (!validatorRequest.IsValid)
			throw new DefaultException(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

		var validateEmail = await repository.FindAccountByEmailAsync(request.Email);
		if (validateEmail is not null)
			throw new DefaultException([MessageException.EMAIL_JA_REGISTRADO]);

		var validateCnpj = await repository.FindAccountByCnpjAsync(request.Cnpj);
		if (validateCnpj is not null)
			throw new DefaultException([MessageException.CNPJ_JA_REGISTRADO]);

		var checkCnpj = await brazilApi.ConsultaCnpj(request.Cnpj);
		if (!checkCnpj)
			throw new DefaultException([MessageException.CNPJ_INVALIDO]);

		if (request.Password != request.PasswordConfirmation)
			throw new DefaultException([MessageException.SENHA_NAO_CONFERE]);

		var company = new Domain.Entities.Account
		{
			Cnpj = request.Cnpj,
			Phone = string.Empty,
			Email = request.Email,
			Password = cryptography.EncryptPassword(request.Password)
		};

		var code = redis.GenerateOneTimePassword(company.AccountId.ToString());

		await repository.CreateAccountAsync(company);

		await sendGridService.SendSignUpAsync(request.Email, code);

		return new ResponseDefault(company.AccountId.ToString(), MessageResponse.CODIGO_ENVIADO_SIGN_UP);
	}
}