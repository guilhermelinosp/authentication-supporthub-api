using Company.SupportHub.Application.Services.Cryptography;
using Company.SupportHub.Application.UseCases.Validators;
using Company.SupportHub.Domain.DTOs.Requests;
using Company.SupportHub.Domain.DTOs.Responses;
using Company.SupportHub.Domain.Exceptions;
using Company.SupportHub.Domain.Repositories;
using Company.SupportHub.Domain.Services;
using Company.SupportHub.Infrastructure.Services;

namespace Company.SupportHub.Application.UseCases.SignIn;

public class SignInUseCase(
	ICompanyRepository repository,
	ICryptographyService cryptographyService,
	ISendGridService sendGridService,
	ITwilioService twilioService,
	IRedisService redis)
	: ISignInUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(RequestSignIn request)
	{
		var validator = await new SignInValidator().ValidateAsync(request);
		if (!validator.IsValid)
			throw new ExceptionDefault(validator.Errors.Select(er => er.ErrorMessage).ToList());

		var account = await repository.FindCompanyByEmailAsync(request.Email);
		if (account is null)
			throw new ExceptionDefault([MessageException.EMAIL_NAO_ENCONTRADO]);

		if (!cryptographyService.VerifyPassword(request.Password, account.Password))
			throw new ExceptionDefault([MessageException.SENHA_INVALIDA]);

		var session = redis.ValidateSessionAccountAsync(account.CompanyId.ToString());
		if (session)
			throw new ExceptionDefault([MessageException.SESSION_ATIVA]);

		var code = redis.GenerateOneTimePassword(account.CompanyId.ToString());

		if (!account.IsVerified)
		{
			await sendGridService.SendSignUpAsync(request.Email, code);
			throw new ExceptionDefault([MessageException.EMAIL_NAO_AUTENTICADO]);
		}

		await repository.UpdateCompanyAsync(account);

		if (account.Is2Fa)
			await twilioService.SendSignInAsync(account.Phone, code);
		else
			await sendGridService.SendSignInAsync(request.Email, code);

		return new ResponseDefault(account.CompanyId.ToString(), MessageResponse.CODIGO_ENVIADO);
	}
}