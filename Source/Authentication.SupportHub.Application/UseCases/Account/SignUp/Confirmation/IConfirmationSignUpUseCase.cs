using Authentication.SupportHub.Domain.DTOs.Responses;

namespace Authentication.SupportHub.Application.UseCases.Account.SignUp.Confirmation;

public interface IConfirmationSignUpUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(string accountId, string code);
}