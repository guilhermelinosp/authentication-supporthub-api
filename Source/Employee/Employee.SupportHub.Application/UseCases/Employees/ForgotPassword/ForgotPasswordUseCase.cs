using Employee.SupportHub.Application.UseCases.Validators;
using Employee.SupportHub.Domain.Cache;
using Employee.SupportHub.Domain.DTOs.Requests;
using Employee.SupportHub.Domain.DTOs.Responses;
using Employee.SupportHub.Domain.Exceptions;
using Employee.SupportHub.Domain.Repositories;
using Employee.SupportHub.Domain.Services;

namespace Employee.SupportHub.Application.UseCases.Employees.ForgotPassword;

public class ForgotPasswordUseCase(
	IEmployeeRepository repository,
	ISendGridService sendGridService,
	IOneTimePasswordCache oneTimePassword)
	: IForgotPasswordUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(RequestForgotPassword request)
	{
		var validatorRequest = await new ForgotPasswordValidator().ValidateAsync(request);
		if (!validatorRequest.IsValid)
			throw new DefaultException(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

		var account = await repository.FindEmployeeByEmailAsync(request.Email);
		if (account is null)
			throw new DefaultException([MessagesException.EMAIL_NAO_ENCONTRADO]);

		var code = oneTimePassword.GenerateOneTimePassword(account.CompanyId.ToString());

		await sendGridService.SendSignInAsync(request.Email, code);

		return new ResponseDefault(account.CompanyId.ToString(), MessagesResponse.CODIGO_ENVIADO);
	}
}