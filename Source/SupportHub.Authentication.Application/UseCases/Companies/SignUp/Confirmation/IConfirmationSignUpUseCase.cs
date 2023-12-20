using SupportHub.Authentication.Domain.DTOs.Responses;

namespace SupportHub.Authentication.Application.UseCases.Companies.SignUp.Confirmation;

public interface IConfirmationSignUpUseCase : IApplicationInjection
{
    Task<ResponseDefault> ExecuteAsync(string accountId, string code);
}