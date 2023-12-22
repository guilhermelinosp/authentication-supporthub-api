using SupportHub.Application.UseCases.Validators;
using SupportHub.Domain.Cache;
using SupportHub.Domain.DTOs.Requests;
using SupportHub.Domain.DTOs.Responses;
using SupportHub.Domain.Exceptions;
using SupportHub.Domain.Repositories;
using SupportHub.Domain.Services;

namespace SupportHub.Application.UseCases.Customers.ForgotPassword;

public class ForgotPasswordUseCase(
	ICompanyRepository repository,
	ISendGridService sendGridService,
	IOneTimePasswordCache oneTimePassword)
	: IForgotPasswordUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(RequestForgotPassword request)
	{
		var validatorRequest = await new ForgotPasswordValidator().ValidateAsync(request);
		if (!validatorRequest.IsValid)
			throw new DefaultException(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

		var account = await repository.FindCompanyByEmailAsync(request.Email);
		if (account is null)
			throw new DefaultException([MessagesException.EMAIL_NAO_ENCONTRADO]);

		var code = oneTimePassword.GenerateOneTimePassword(account.CompanyId.ToString());

		await sendGridService.SendSignInAsync(request.Email, code);

		return new ResponseDefault(account.CompanyId.ToString(), MessagesResponse.CODIGO_ENVIADO);
	}
}