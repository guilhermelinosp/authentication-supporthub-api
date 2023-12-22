using SupportHub.Authentication.Application.Services.Cryptography;
using SupportHub.Authentication.Application.UseCases.Companies.Validators;
using SupportHub.Domain.Cache;
using SupportHub.Domain.DTOs.Requests.Companies;
using SupportHub.Domain.DTOs.Responses;
using SupportHub.Domain.Exceptions;
using SupportHub.Domain.Repositories;
using SupportHub.Domain.Services;

namespace SupportHub.Authentication.Application.UseCases.Companies.SignIn;

public class SignInUseCase(
	ICompanyRepository repository,
	ICryptographyService cryptographyService,
	ISendGridService sendGridService,
	ITwilioService twilioService,
	ISessionCache sessionCache,
	IOneTimePasswordCache oneTimePassword)
	: ISignInUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(RequestSignIn request)
	{
		var validator = await new SignInValidator().ValidateAsync(request);
		if (!validator.IsValid)
			throw new DefaultException(validator.Errors.Select(er => er.ErrorMessage).ToList());

		var account = await repository.FindCompanyByEmailAsync(request.Email);
		if (account is null)
			throw new DefaultException([MessagesException.EMAIL_NAO_ENCONTRADO]);

		if (!cryptographyService.VerifyPassword(request.Password, account.Password))
			throw new DefaultException([MessagesException.SENHA_INVALIDA]);

		var session = sessionCache.ValidateSessionAsync(account.CompanyId.ToString());
		if (session)
			throw new DefaultException([MessagesException.SESSION_ATIVA]);

		var code = oneTimePassword.GenerateOneTimePassword(account.CompanyId.ToString());

		if (!account.IsVerified)
		{
			await sendGridService.SendSignUpAsync(request.Email, code);
			throw new DefaultException([MessagesException.EMAIL_NAO_AUTENTICADO]);
		}

		await repository.UpdateCompanyAsync(account);

		if (account.Is2Fa)
			await twilioService.SendSignInAsync(account.Phone, code);
		else
			await sendGridService.SendSignInAsync(request.Email, code);

		return new ResponseDefault(account.CompanyId.ToString(), MessagesResponse.CODIGO_ENVIADO);
	}
}