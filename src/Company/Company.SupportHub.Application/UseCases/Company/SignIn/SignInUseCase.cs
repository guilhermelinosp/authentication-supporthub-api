using Company.SupportHub.Application.Services.Cryptography;
using Company.SupportHub.Application.UseCases.Validators;
using Company.SupportHub.Domain.DTOs.Requests;
using Company.SupportHub.Domain.DTOs.Responses;
using Company.SupportHub.Domain.Exceptions;
using Company.SupportHub.Domain.Messages;
using Company.SupportHub.Domain.Repositories;
using Company.SupportHub.Domain.Services;

namespace Company.SupportHub.Application.UseCases.Company.SignIn;

public class SignInUseCase(
	ICompanyRepository repository,
	ICryptographyService cryptographyService,
	ISendGridService sendGridService,
	ITwilioService twilioService,
	IRedisService redis)
	: ISignInUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(RequestSignInCompany request)
	{
		var validator = await new ValidatorSignInCompany().ValidateAsync(request);
		if (!validator.IsValid)
			throw new DefaultException(validator.Errors.Select(er => er.ErrorMessage).ToList());

		var account = await repository.FindCompanyByCnpjAsync(request.Cnpj);
		if (account is null)
			throw new DefaultException([MessageException.EMAIL_NAO_ENCONTRADO]);

		if (!cryptographyService.VerifyPassword(request.Password, account.Password))
			throw new DefaultException([MessageException.SENHA_INVALIDA]);

		var code = redis.GenerateOneTimePassword(account.CompanyId.ToString());

		if (!account.IsVerified)
		{
			await sendGridService.SendSignUpAsync(account.Email, code);
			throw new DefaultException([MessageException.EMAIL_NAO_AUTENTICADO]);
		}

		await repository.UpdateCompanyAsync(account);

		if (account.Is2Fa)
			await twilioService.SendSignInAsync(account.Phone, code);
		else
			await sendGridService.SendSignInAsync(account.Email, code);

		return new ResponseDefault(account.CompanyId.ToString(), MessageResponse.CODIGO_ENVIADO);
	}
}