using Authentication.SupportHub.Domain.DTOs.Requests;
using Authentication.SupportHub.Domain.DTOs.Responses;

namespace Authentication.SupportHub.Application.UseCases.Account.SignIn.Confirmation;

public interface IConfirmationSignInUseCase
{
	ResponseToken ExecuteAsync(string accountId, string code);
}