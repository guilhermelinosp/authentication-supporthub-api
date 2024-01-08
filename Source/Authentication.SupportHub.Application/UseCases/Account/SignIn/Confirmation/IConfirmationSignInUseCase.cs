using Authentication.SupportHub.Domain.DTOs.Responses;

namespace Authentication.SupportHub.Application.UseCases.Account.SignIn.Confirmation;

public interface IConfirmationSignInUseCase : IApplicationInjection
{
	ResponseToken ExecuteAsync(string accountId, string code);
}