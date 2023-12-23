using Company.SupportHub.Domain.DTOs.Responses;
using Company.SupportHub.Domain.Exceptions;
using Company.SupportHub.Domain.Repositories;
using Company.SupportHub.Infrastructure.Services;

namespace Company.SupportHub.Application.UseCases.SignUp.Confirmation;

public class ConfirmationSignUpUseCase(
	ICompanyRepository repository,
	IRedisService redis)
	: IConfirmationSignUpUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(string accountId, string code)
	{
		var validatorCode = redis.ValidateOneTimePassword(accountId, code);
		if (!validatorCode)
			throw new ExceptionDefault([MessageException.CODIGO_INVALIDO]);

		var account = await repository.FindCompanyByIdAsync(Guid.Parse(accountId));
		if (account is null)
			throw new ExceptionDefault([MessageException.CONTA_NAO_ENCONTRADA]);

		account.IsVerified = true;

		await repository.UpdateCompanyAsync(account);

		return new ResponseDefault(accountId, MessageResponse.CODIGO_CONFIRMADO);
	}
}