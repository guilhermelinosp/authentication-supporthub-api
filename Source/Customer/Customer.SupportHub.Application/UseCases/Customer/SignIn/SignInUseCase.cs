using Customer.SupportHub.Application.Services.Cryptography;
using Customer.SupportHub.Application.UseCases.Validators;
using Customer.SupportHub.Domain.DTOs.Requests;
using Customer.SupportHub.Domain.DTOs.Responses;
using Customer.SupportHub.Domain.Exceptions;
using Customer.SupportHub.Domain.Messages;
using Customer.SupportHub.Domain.Repositories;
using Customer.SupportHub.Domain.Services;

namespace Customer.SupportHub.Application.UseCases.Customer.SignIn;

public class SignInUseCase(
	ICustomerRepository repository,
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

		var account = await repository.FindCustomerByEmailAsync(request.Email);
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

		await repository.UpdateCustomerAsync(account);

		if (account.Is2Fa)
			await twilioService.SendSignInAsync(account.Phone, code);
		else
			await sendGridService.SendSignInAsync(request.Email, code);

		return new ResponseDefault(account.CompanyId.ToString(), MessageResponse.CODIGO_ENVIADO);
	}
}