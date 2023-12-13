using SupportHub.Auth.Domain.DTOs.Responses;

namespace SupportHub.Auth.Application.UseCases.Companies.SignIn.Confirmation;

public interface IConfirmationSignInUseCase : IApplicationInjection
{
    ResponseToken ExecuteAsync(string accountId, string code);
}