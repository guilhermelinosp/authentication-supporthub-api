using SupportHub.Application.Services.Cryptography;
using SupportHub.Application.UseCases.Validators;
using SupportHub.Domain.Cache;
using SupportHub.Domain.DTOs.Requests;
using SupportHub.Domain.DTOs.Responses;
using SupportHub.Domain.Exceptions;
using SupportHub.Domain.Repositories;

namespace SupportHub.Application.UseCases.Employees.ForgotPassword.Confirmation;

public class ResetPasswordUseCase(
	IEmployeeRepository repository,
	ICryptographyService cryptography,
	IOneTimePasswordCache oneTimePassword)
	: Companies.ForgotPassword.Confirmation.IResetPasswordUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(RequestResetPassword request, string accountId, string code)
	{
		var validatorRequest = await new ResetPasswordValidator().ValidateAsync(request);
		if (!validatorRequest.IsValid)
			throw new DefaultException(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

		var validatorCode = oneTimePassword.ValidateOneTimePassword(accountId, code);
		if (!validatorCode)
			throw new DefaultException([MessagesException.CODIGO_INVALIDO]);

		var account = await repository.FindEmployeeByIdAsync(Guid.Parse(accountId));
		if (account is null)
			throw new DefaultException([MessagesException.CONTA_NAO_ENCONTRADA]);

		if (request.Password != request.PasswordConfirmation)
			throw new DefaultException([MessagesException.SENHA_NAO_CONFERE]);

		account.Password = cryptography.EncryptPassword(request.Password!);

		await repository.UpdateEmployeeAsync(account);

		return new ResponseDefault(accountId, MessagesResponse.SENHA_RESETADA);
	}
}