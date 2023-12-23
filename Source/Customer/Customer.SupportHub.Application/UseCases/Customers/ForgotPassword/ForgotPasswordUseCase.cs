using Customer.SupportHub.Application.UseCases.Validators;
using Customer.SupportHub.Domain.Cache;
using Customer.SupportHub.Domain.DTOs.Requests;
using Customer.SupportHub.Domain.DTOs.Responses;
using Customer.SupportHub.Domain.Exceptions;
using Customer.SupportHub.Domain.Repositories;
using Customer.SupportHub.Domain.Services;

namespace Customer.SupportHub.Application.UseCases.Customers.ForgotPassword;

public class ForgotPasswordUseCase(
	ICustomerRepository repository,
	ISendGridService sendGridService,
	IOneTimePasswordCache oneTimePassword)
	: IForgotPasswordUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(RequestForgotPassword request)
	{
		var validatorRequest = await new ForgotPasswordValidator().ValidateAsync(request);
		if (!validatorRequest.IsValid)
			throw new DefaultException(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

		var account = await repository.FindCustomerByEmailAsync(request.Email);
		if (account is null)
			throw new DefaultException([MessagesException.EMAIL_NAO_ENCONTRADO]);

		var code = oneTimePassword.GenerateOneTimePassword(account.CompanyId.ToString());

		await sendGridService.SendSignInAsync(request.Email, code);

		return new ResponseDefault(account.CompanyId.ToString(), MessagesResponse.CODIGO_ENVIADO);
	}
}