using Authentication.SupportHub.Domain.DTOs.Requests;
using Authentication.SupportHub.Domain.DTOs.Responses;

namespace Authentication.SupportHub.Application.UseCases.Account.ResetPassword;

public interface IResetPasswordUseCase
{
	Task<ResponseDefault> ExecuteAsync(RequestResetPassword request, string accountId, string code);
}