using SupportHub.Domain.DTOs.Requests;
using SupportHub.Domain.DTOs.Responses;

namespace SupportHub.Application.UseCases.Customers.ForgotPassword.Confirmation;

public interface IResetPasswordUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(RequestResetPassword request, string accountId, string code);
}