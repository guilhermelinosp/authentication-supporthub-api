using Customer.SupportHub.Application.Services.Cryptography;
using Customer.SupportHub.Application.UseCases.Validators;
using Customer.SupportHub.Domain.DTOs.Requests;
using Customer.SupportHub.Domain.DTOs.Responses;
using Customer.SupportHub.Domain.Exceptions;
using Customer.SupportHub.Domain.Messages;
using Customer.SupportHub.Domain.Repositories;
using Customer.SupportHub.Domain.Services;

namespace Customer.SupportHub.Application.UseCases.Customer.ForgotPassword.Confirmation;

public class ResetPasswordUseCase(
	ICustomerRepository repository,
	ICryptographyService cryptography,
	IRedisService redis)
	: IResetPasswordUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(RequestResetPassword request, string accountId, string code)
	{
		var validatorRequest = await new ResetPasswordValidator().ValidateAsync(request);
		if (!validatorRequest.IsValid)
			throw new ExceptionDefault(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

		var validatorCode = redis.ValidateOneTimePassword(accountId, code);
		if (!validatorCode)
			throw new ExceptionDefault([MessageException.CODIGO_INVALIDO]);

		var account = await repository.FindCustomerByIdAsync(Guid.Parse(accountId));
		if (account is null)
			throw new ExceptionDefault([MessageException.CONTA_NAO_ENCONTRADA]);

		if (request.Password != request.PasswordConfirmation)
			throw new ExceptionDefault([MessageException.SENHA_NAO_CONFERE]);

		account.Password = cryptography.EncryptPassword(request.Password!);

		await repository.UpdateCustomerAsync(account);

		return new ResponseDefault(accountId, MessageResponse.SENHA_RESETADA);
	}
}