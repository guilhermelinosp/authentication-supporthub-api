using Company.SupportHub.Application.UseCases.Validators;
using Company.SupportHub.Domain.DTOs.Requests;
using Company.SupportHub.Domain.DTOs.Responses;
using Company.SupportHub.Domain.Exceptions;
using Company.SupportHub.Domain.Repositories;
using Company.SupportHub.Domain.Services;
using Company.SupportHub.Infrastructure.Services;

namespace Company.SupportHub.Application.UseCases.ForgotPassword;

public class ForgotPasswordUseCase(
	ICompanyRepository repository,
	ISendGridService sendGridService,
	ITwilioService twilioService,
	IRedisService redis)
	: IForgotPasswordUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(RequestForgotPassword request)
	{
		var validatorRequest = await new ForgotPasswordValidator().ValidateAsync(request);
		if (!validatorRequest.IsValid)
			throw new ExceptionDefault(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

		var account = await repository.FindCompanyByEmailAsync(request.Email);
		if (account is null)
			throw new ExceptionDefault([MessageException.EMAIL_NAO_ENCONTRADO]);

		var code = redis.GenerateOneTimePassword(account.CompanyId.ToString());

		if (account.Is2Fa)
			await twilioService.SendSignInAsync(account.Phone, code);
		else
			await sendGridService.SendSignInAsync(request.Email, code);

		return new ResponseDefault(account.CompanyId.ToString(), MessageResponse.CODIGO_ENVIADO);
	}
}