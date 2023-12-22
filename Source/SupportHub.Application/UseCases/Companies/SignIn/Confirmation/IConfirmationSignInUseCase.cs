using SupportHub.Domain.DTOs.Responses;

namespace SupportHub.Authentication.Application.UseCases.Companies.SignIn.Confirmation;

public interface IConfirmationSignInUseCase : IApplicationInjection
{
	ResponseToken ExecuteAsync(string accountId, string code);
}