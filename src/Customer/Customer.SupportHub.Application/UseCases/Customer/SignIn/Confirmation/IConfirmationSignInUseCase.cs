using Customer.SupportHub.Domain.DTOs.Responses;

namespace Customer.SupportHub.Application.UseCases.Customer.SignIn.Confirmation;

public interface IConfirmationSignInUseCase : IApplicationInjection
{
	ResponseToken ExecuteAsync(string accountId, string code);
}