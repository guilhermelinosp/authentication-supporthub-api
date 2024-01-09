using Authentication.SupportHub.Domain.DTOs.Requests;
using Authentication.SupportHub.Domain.DTOs.Responses;

namespace Authentication.SupportHub.Application.UseCases.Account.SignUp.Confirmation;

public interface IConfirmationSignUpUseCase
{
	Task<ResponseDefault> ExecuteAsync(string accountId, string code);
}