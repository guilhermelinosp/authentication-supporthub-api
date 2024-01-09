using Authentication.SupportHub.Application.UseCases.Validators;
using Authentication.SupportHub.Domain.DTOs.Requests;
using Authentication.SupportHub.Domain.DTOs.Responses;
using Authentication.SupportHub.Domain.Exceptions;
using Authentication.SupportHub.Domain.Messages;
using Authentication.SupportHub.Domain.Repositories;
using Authentication.SupportHub.Domain.Services;

namespace Authentication.SupportHub.Application.UseCases.Account.ForgotPassword;

public class ForgotPasswordUseCase(
	IAccountRepository repository,
	ISendGridService sendGridService,
	ITwilioService twilioService,
	IRedisService redis)
	: IForgotPasswordUseCase
{
	public async Task<ResponseDefault> ExecuteAsync(RequestForgotPassword request)
	{
		var validatorRequest = await new ValidatorForgotPassword().ValidateAsync(request);
		if (!validatorRequest.IsValid)
			throw new DefaultException(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

		var account = await repository.FindAccountByEmailAsync(request.Email);
		if (account is null)
			throw new DefaultException([MessageException.EMAIL_NAO_ENCONTRADO]);

		var code = redis.GenerateOneTimePassword(account.AccountId.ToString());

		if (account.Is2Fa)
			await twilioService.SendSignInAsync(account.Phone, code);
		else
			await sendGridService.SendSignInAsync(request.Email, code);

		return new ResponseDefault
		{
			Message = MessageResponse.CODIGO_ENVIADO,
			AccountId = account.AccountId.ToString()
		};
	}
}