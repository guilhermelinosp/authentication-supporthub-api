using Company.SupportHub.Application.Services.Cryptography;
using Company.SupportHub.Application.UseCases.Validators;
using Company.SupportHub.Domain.DTOs.Requests;
using Company.SupportHub.Domain.DTOs.Responses;
using Company.SupportHub.Domain.Exceptions;
using Company.SupportHub.Domain.Messages;
using Company.SupportHub.Domain.Repositories;
using Company.SupportHub.Domain.Services;

namespace Company.SupportHub.Application.UseCases.Company.ForgotPassword.Confirmation;

public class ResetPasswordUseCase(
	ICompanyRepository repository,
	ICryptographyService cryptography,
	IRedisService redis)
	: IResetPasswordUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(RequestResetPassword request, string accountId, string code)
	{
		var validatorRequest = await new ResetPasswordValidator().ValidateAsync(request);
		if (!validatorRequest.IsValid)
			throw new DefaultException(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

		var validatorCode = redis.ValidateOneTimePassword(accountId, code);
		if (!validatorCode)
			throw new DefaultException([MessageException.CODIGO_INVALIDO]);

		var account = await repository.FindCompanyByIdAsync(Guid.Parse(accountId));
		if (account is null)
			throw new DefaultException([MessageException.CONTA_NAO_ENCONTRADA]);

		if (request.Password != request.PasswordConfirmation)
			throw new DefaultException([MessageException.SENHA_NAO_CONFERE]);

		account.Password = cryptography.EncryptPassword(request.Password!);

		await repository.UpdateCompanyAsync(account);

		return new ResponseDefault(accountId, MessageResponse.SENHA_RESETADA);
	}
}