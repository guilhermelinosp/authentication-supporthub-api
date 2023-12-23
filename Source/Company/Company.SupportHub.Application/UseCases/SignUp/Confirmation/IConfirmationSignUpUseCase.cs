using Company.SupportHub.Domain.DTOs.Responses;

namespace Company.SupportHub.Application.UseCases.SignUp.Confirmation;

public interface IConfirmationSignUpUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(string accountId, string code);
}