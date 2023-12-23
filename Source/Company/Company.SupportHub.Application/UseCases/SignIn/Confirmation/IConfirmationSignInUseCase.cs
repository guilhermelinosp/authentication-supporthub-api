using Company.SupportHub.Domain.DTOs.Responses;

namespace Company.SupportHub.Application.UseCases.SignIn.Confirmation;

public interface IConfirmationSignInUseCase : IApplicationInjection
{
	ResponseToken ExecuteAsync(string accountId, string code);
}