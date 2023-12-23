using Customer.SupportHub.Application.UseCases.Validators;
using Customer.SupportHub.Domain.DTOs.Requests;
using Customer.SupportHub.Domain.DTOs.Responses;
using Customer.SupportHub.Domain.Exceptions;
using Customer.SupportHub.Domain.Repositories;
using Customer.SupportHub.Domain.Services;

namespace Customer.SupportHub.Application.UseCases.ForgotPassword;

public class ForgotPasswordUseCase(
	ICustomerRepository repository,
	ISendGridService sendGrid,
	IRedisService redis)
	: IForgotPasswordUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(RequestForgotPassword request)
	{
		var validatorRequest = await new ForgotPasswordValidator().ValidateAsync(request);
		if (!validatorRequest.IsValid)
			throw new ExceptionDefault(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

		var account = await repository.FindCustomerByEmailAsync(request.Email);
		if (account is null)
			throw new ExceptionDefault([MessageException.EMAIL_NAO_ENCONTRADO]);

		var code = redis.GenerateOneTimePassword(account.CompanyId.ToString());

		await sendGrid.SendSignInAsync(request.Email, code);

		return new ResponseDefault(account.CompanyId.ToString(), MessageResponse.CODIGO_ENVIADO);
	}
}