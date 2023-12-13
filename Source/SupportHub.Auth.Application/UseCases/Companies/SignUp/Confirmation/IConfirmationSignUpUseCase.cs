using SupportHub.Auth.Domain.DTOs.Responses;

namespace SupportHub.Auth.Application.UseCases.Companies.SignUp.Confirmation;

public interface IConfirmationSignUpUseCase : IApplicationInjection
{
    Task<ResponseDefault> ExecuteAsync(string accountId, string code);
}