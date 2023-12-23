using Employee.SupportHub.Application.Services.Cryptography;
using Employee.SupportHub.Application.UseCases.Validators;
using Employee.SupportHub.Domain.DTOs.Requests;
using Employee.SupportHub.Domain.DTOs.Responses;
using Employee.SupportHub.Domain.Exceptions;
using Employee.SupportHub.Domain.Messages;
using Employee.SupportHub.Domain.Repositories;
using Employee.SupportHub.Domain.Services;

namespace Employee.SupportHub.Application.UseCases.ForgotPassword.Confirmation;

public class ResetPasswordUseCase(
	IEmployeeRepository repository,
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

		var account = await repository.FindEmployeeByIdAsync(Guid.Parse(accountId));
		if (account is null)
			throw new ExceptionDefault([MessageException.CONTA_NAO_ENCONTRADA]);

		if (request.Password != request.PasswordConfirmation)
			throw new ExceptionDefault([MessageException.SENHA_NAO_CONFERE]);

		account.Password = cryptography.EncryptPassword(request.Password!);

		await repository.UpdateEmployeeAsync(account);

		return new ResponseDefault(accountId, MessageResponse.SENHA_RESETADA);
	}
}