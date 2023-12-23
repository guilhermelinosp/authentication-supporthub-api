using Company.SupportHub.Domain.DTOs.Requests;
using Company.SupportHub.Domain.DTOs.Responses;

namespace Company.SupportHub.Application.UseCases.ForgotPassword.Confirmation;

public interface IResetPasswordUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(RequestResetPassword request, string accountId, string code);
}