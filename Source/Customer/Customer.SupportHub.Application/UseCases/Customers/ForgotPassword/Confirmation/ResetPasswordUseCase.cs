using Customer.SupportHub.Application.Services.Cryptography;
using Customer.SupportHub.Application.UseCases.Validators;
using Customer.SupportHub.Domain.Cache;
using Customer.SupportHub.Domain.DTOs.Requests;
using Customer.SupportHub.Domain.DTOs.Responses;
using Customer.SupportHub.Domain.Exceptions;
using Customer.SupportHub.Domain.Repositories;

namespace Customer.SupportHub.Application.UseCases.Customers.ForgotPassword.Confirmation;

public class ResetPasswordUseCase(
	ICustomerRepository repository,
	ICryptographyService cryptography,
	IOneTimePasswordCache oneTimePassword)
	: IResetPasswordUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(RequestResetPassword request, string accountId, string code)
	{
		var validatorRequest = await new ResetPasswordValidator().ValidateAsync(request);
		if (!validatorRequest.IsValid)
			throw new DefaultException(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

		var validatorCode = oneTimePassword.ValidateOneTimePassword(accountId, code);
		if (!validatorCode)
			throw new DefaultException([MessagesException.CODIGO_INVALIDO]);

		var account = await repository.FindCustomerByIdAsync(Guid.Parse(accountId));
		if (account is null)
			throw new DefaultException([MessagesException.CONTA_NAO_ENCONTRADA]);

		if (request.Password != request.PasswordConfirmation)
			throw new DefaultException([MessagesException.SENHA_NAO_CONFERE]);

		account.Password = cryptography.EncryptPassword(request.Password!);

		await repository.UpdateCustomerAsync(account);

		return new ResponseDefault(accountId, MessagesResponse.SENHA_RESETADA);
	}
}